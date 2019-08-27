using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.Documents.Queries;
using DT.Core.Data.Models;
using DT.Core.Web.Common.Identity.Extensions;
using MultipartDataMediaFormatter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class DocumentsApiController : DocumentManagementApiControllerBase
    {
        private readonly string UploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
        public DocumentsApiController()
        {
        }

        [Route("api/documents/searchdocumentsbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocuments)]
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest, string token, bool advancedSearch)
        {
            return await Mediator.Send(new SearchDocumentsByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token,
                AdvancedSearch = advancedSearch
            });
        }

        [Route("api/documents/searchdocumentsbydtandtokenpaged")]
        [HttpGet]
        public async Task<DataSourceResult> SearchDocumentsByTypeAndToken([FromUri]DataSourceRequest dataSourceRequest, string token, bool advancedSearch, string documentType)
        {
            return await Mediator.Send(new SearchDocumentsByDocumentTypeAndTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token,
                Department = User.Identity.GetDepartment(),
                AdvancedSearch = advancedSearch,
                DocumentType = documentType
            });
        }

        public class JqGridRequest
        {
            public string Sidx { get; set; }
            public int Rows { get; set; }
            public int Page { get; set; }
            public string Sord { get; set; }
            public string Token { get; set; }
            public bool AdvancedSearch { get; set; }
            public string DocumentType { get; set; }
        }

        [Route("api/documents/searchdocumentsforjqgrid")]
        [HttpGet]
        public async Task<DataSourceResult> SearchDocumentsByTypeAndTokenForJqGrid([FromUri]JqGridRequest request)
        {
            DataSourceRequest dataSourceRequest = new DataSourceRequest
            {
                SortDataField = "name",
                SortOrder = request.Sord,
                PageSize = request.Rows,
                PageNum = request.Page - 1
            };
            return await Mediator.Send(new SearchDocumentsByDocumentTypeAndTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = request.Token,
                AdvancedSearch = request.AdvancedSearch,
                DocumentType = request.DocumentType
            });
        }

        public class PqGridRequest
        {
            public string Sidx { get; set; }
            public int Pq_CurPage { get; set; }
            public int Pq_rpp { get; set; }
            public string Sord { get; set; }
            public string Token { get; set; }
            public bool AdvancedSearch { get; set; }
            public string DocumentType { get; set; }
        }

        [Route("api/documents/searchdocumentsforpqgrid")]
        [HttpGet]
        public async Task<DataSourceResult> SearchDocumentsByTypeAndTokenForPqGrid([FromUri]PqGridRequest request)
        {
            DataSourceRequest dataSourceRequest = new DataSourceRequest
            {
                SortDataField = "name",
                SortOrder = "asc",
                PageSize = request.Pq_rpp,
                PageNum = request.Pq_CurPage - 1
            };
            return await Mediator.Send(new SearchDocumentsByDocumentTypeAndTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = request.Token,
                AdvancedSearch = request.AdvancedSearch,
                DocumentType = request.DocumentType
            });
        }

        [Route("api/documents/getalldocuments")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocumentTypes)]
        public async Task<List<GetAllDocumentDto>> GetAlls()
        {
            return await Mediator.Send(new GetAllDocumentQuery());
        }

        [Route("api/documents/create")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocuments)]
        public async Task<int> Create([FromBody]CreateDocumentCommand createDocumentCommand)
        {
            createDocumentCommand.CreatedBy = User.Identity.GetUserName();
            createDocumentCommand.CreatedOn = DateTime.Now;
            createDocumentCommand.Deleted = false;
            createDocumentCommand.FileName = await UploadDocuments(createDocumentCommand);

            return await Mediator.Send(createDocumentCommand);
        }

        [Route("api/documents/createandrelease")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocuments)]
        public async Task<int> CreateAndRelease([FromBody]CreateDocumentCommand createDocumentCommand)
        {
            createDocumentCommand.CreatedBy = User.Identity.GetUserName();
            createDocumentCommand.CreatedOn = DateTime.Now;
            createDocumentCommand.Deleted = false;
            createDocumentCommand.FileName = await UploadDocuments(createDocumentCommand);

            int id = await Mediator.Send(createDocumentCommand);
            if (id > 0)
            {
                await SendMailReleaseDocument(createDocumentCommand);
            }
            return id;
        }

        [Route("api/documents/release")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocuments)]
        public async Task<int> Release([FromBody]GetDocumentByIdDto documentByIdDto)
        {
            if (documentByIdDto.Id > 0)
            {
                GetDocumentByIdDto document = await Mediator.Send(new GetDocumentByIdQuery
                {
                    Id = documentByIdDto.Id
                });

                if (document != null && document.Id > 0)
                {
                    return await SendMailReleaseDocument(document);
                }
                else
                {
                    throw new NullReferenceException("Không tìm thấy tài liệu");
                }
            }
            throw new ArgumentNullException(nameof(documentByIdDto.Id), "Id is not null");
        }

        [Route("api/documents/update")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.ApiDocuments)]
        public async Task<int> Update([FromBody]UpdateDocumentCommand updateDocumentCommand)
        {
            updateDocumentCommand.ModifiedBy = User.Identity.GetUserName();
            updateDocumentCommand.ModifiedOn = DateTime.Now;
            updateDocumentCommand.Deleted = false;
            updateDocumentCommand.FileName = await UploadDocuments(updateDocumentCommand);

            return await Mediator.Send(updateDocumentCommand);
        }

        [Route("api/documents/review")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocuments)]
        public async Task<int> Review([FromBody]ReviewDocumentCommand reviewDocumentCommand)
        {
            reviewDocumentCommand.CreatedBy = User.Identity.GetUserName();
            reviewDocumentCommand.CreatedOn = DateTime.Now;
            reviewDocumentCommand.Deleted = false;
            reviewDocumentCommand.FileName = await UploadDocuments(reviewDocumentCommand);

            return await Mediator.Send(reviewDocumentCommand);
        }

        [Route("api/documents/reviewandrelease")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocuments)]
        public async Task<int> ReviewAndRelease([FromBody]ReviewDocumentCommand reviewDocumentCommand)
        {
            reviewDocumentCommand.CreatedBy = User.Identity.GetUserName();
            reviewDocumentCommand.CreatedOn = DateTime.Now;
            reviewDocumentCommand.Deleted = false;
            reviewDocumentCommand.FileName = await UploadDocuments(reviewDocumentCommand);

            int id = await Mediator.Send(reviewDocumentCommand);
            if (id > 0)
            {
                await SendMailReviewDocument(reviewDocumentCommand);
            }
            return id;
        }

        [Route("api/documents/deletefile")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.ApiDocuments)]
        public async Task<int> DeleteFile([FromBody]DeleteFileByIdAndFileNameCommand deleteFileByIdAndFileNameCommand)
        {
            return await Mediator.Send(deleteFileByIdAndFileNameCommand);
        }

        [Route("api/documents/sendmaildocumentbymonth")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocumentTypes)]
        public async Task<int> SendMailDocumentByMonth(GetDocumentsByMonthQuery query)
        {
            List<GetDocumentsByMonthDto> documents = await Mediator.Send(query);
            await SendMailDocumentsỊnMonth(documents, query.Month, query.Year);
            return 1;
        }

        public void SaveAs(string filePath, HttpFile httpFile)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (MemoryStream memoryStream = new MemoryStream(httpFile.Buffer))
                {
                    memoryStream.WriteTo(file);
                }
            }
        }

        private Task<string> UploadDocuments(CreateDocumentCommand command)
        {
            List<string> files = new List<string>();
            string folderPath = GetFolderPath(command.Code);
            if (command.Files != null && command.Files.Any())
            {
                foreach (HttpFile file in command.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    command.FolderName = folderPath.Replace(UploadFolderPath, string.Empty);
                    command.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    SaveAs(filePath, file);
                }
            }

            if (command.AppendiceFiles != null && command.AppendiceFiles.Any())
            {
                int index = 0;
                foreach (HttpFile file in command.AppendiceFiles)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    SaveAs(filePath, file);
                    command.Appendices[index].LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                    index++;
                }
            }
            return Task.FromResult(string.Join(";", files));
        }

        private Task<string> UploadDocuments(UpdateDocumentCommand command)
        {
            string folderPath = GetFolderPath(command.Code);
            command.FolderName = folderPath.Replace(UploadFolderPath, string.Empty);
            List<string> files = new List<string>();
            if (command.Files != null && command.Files.Any())
            {
                foreach (HttpFile file in command.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    command.FolderName = folderPath.Replace(UploadFolderPath, string.Empty);
                    command.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    SaveAs(filePath, file);
                }
            }

            if (command.AppendiceFiles != null && command.AppendiceFiles.Any())
            {
                foreach (HttpFile file in command.AppendiceFiles)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    SaveAs(filePath, file);
                    Application.Documents.Commands.AppendiceDto appendice = command.Appendices?.FirstOrDefault(a => a.FileName == file.FileName);
                    if (appendice != null)
                        appendice.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                }
            }
            return Task.FromResult(string.Join(";", files));
        }

        private Task<string> UploadDocuments(ReviewDocumentCommand command)
        {
            List<string> files = new List<string>();
            string folderPath = GetFolderPath(command.Code);
            if (command.Files != null && command.Files.Any())
            {
                foreach (HttpFile file in command.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    command.FolderName = folderPath.Replace(UploadFolderPath, string.Empty);
                    command.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    SaveAs(filePath, file);
                }
            }

            if (command.AppendiceFiles != null && command.AppendiceFiles.Any())
            {
                foreach (HttpFile file in command.AppendiceFiles)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    SaveAs(filePath, file);
                    Application.Documents.Commands.AppendiceDto appendice = command.Appendices?.FirstOrDefault(a => a.FileName == file.FileName);
                    if (appendice != null)
                        appendice.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                }
            }
            return Task.FromResult(string.Join(";", files));
        }
    }
}