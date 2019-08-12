using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.Documents.Queries;
using DT.Core.Data.Models;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using DT.Core.Web.Common.Api.WebApi.Formatter;
using DT.Core.Web.Common.Identity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class DocumentsApiController : BaseApiController
    {
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
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocuments)]
        public async Task<DataSourceResult> SearchDocumentsByTypeAndToken([FromUri]DataSourceRequest dataSourceRequest, string token, bool advancedSearch, string documentType)
        {
            return await Mediator.Send(new SearchDocumentsByDocumentTypeAndTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token,
                AdvancedSearch = advancedSearch,
                DocumentType = documentType
            });
        }


        [Route("api/documents/getalldocuments")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocumentTypes)]
        public async Task<List<GetAllDocumentDto>> GetAlls()
        {
            /*List<GetAllDocumentDto> documents = await Mediator.Send(new GetAllDocumentQuery());
            return documents.Select(document => new GetAllDocumentDto
            {
                Id= document.Id,
                Code = document.Code,
                Name = $"{document.Name} {document.DocumentNumber} {document.ReviewNumber}",
                DocumentNumber = document.DocumentNumber,
                ReviewNumber = document.ReviewNumber,
            }).ToList();*/
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
            reviewDocumentCommand.FileName = await ReviewDocuments(reviewDocumentCommand);

            return await Mediator.Send(reviewDocumentCommand);
        }

        [Route("api/documents/deletefile")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.ApiDocuments)]
        public async Task<int> DeleteFile([FromBody]DeleteFileByIdAndFileNameCommand deleteFileByIdAndFileNameCommand)
        {
            return await Mediator.Send(deleteFileByIdAndFileNameCommand);
        }

        private Task<string> UploadDocuments(CreateDocumentCommand command)
        {
            List<string> files = new List<string>();
            if (command.Files.Any())
            {
                foreach (HttpPostedFileMultipart file in command.Files)
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + $"Uploads/{command.DocumentType}/{file.FileName}");
                    files.Add(file.FileName);
                    file.SaveAs(filePath);
                }
            }
            return Task.FromResult(string.Join(";", files));
        }

        private Task<string> UploadDocuments(UpdateDocumentCommand command)
        {
            List<string> files = new List<string>();
            if (command.Files.Any())
            {
                foreach (HttpPostedFileMultipart file in command.Files)
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + $"Uploads/{command.DocumentType}/{file.FileName}");
                    files.Add(file.FileName);
                    file.SaveAs(filePath);
                }
            }
            return Task.FromResult(string.Join(";", files));
        }

        private Task<string> ReviewDocuments(ReviewDocumentCommand command)
        {
            List<string> files = new List<string>();
            if (command.Files.Any())
            {
                foreach (HttpPostedFileMultipart file in command.Files)
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + $"Uploads/{command.DocumentType}/{file.FileName}");
                    files.Add(file.FileName);
                    file.SaveAs(filePath);
                }
            }
            return Task.FromResult(string.Join(";", files));
        }
    }
}