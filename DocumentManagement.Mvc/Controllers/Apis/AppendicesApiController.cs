using DocumentManagement.Application.Appendices.Commands;
using DocumentManagement.Application.Appendices.Queries;
using DocumentManagement.Application.Documents.Commands;
using DT.Core.Data.Models;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using DT.Core.Web.Common.Identity.Extensions;
using MultipartDataMediaFormatter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    public class AppendicesApiController : DocumentManagementApiControllerBase
    {
        private readonly string UploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];

        [Route("api/appendices/searchappendicesbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiAppendices)]
        public async Task<DataSourceResult> SearchAppendicesByTokenPagedQuery([FromUri]DataSourceRequest dataSourceRequest, string token, bool advancedSearch)
        {
            return await Mediator.Send(new SearchAppendicesByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token,
                AdvancedSearch = advancedSearch
            });
        }

        [Route("api/appendices/create")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiAppendices)]
        public async Task<int> Create([FromBody]CreateAppendiceCommand createAppendiceCommand)
        {
            createAppendiceCommand.CreatedBy = User.Identity.GetUserName();
            createAppendiceCommand.CreatedOn = DateTime.Now;
            createAppendiceCommand.Deleted = false;
            createAppendiceCommand.FileName = await UploadAppendices(createAppendiceCommand);

            return await Mediator.Send(createAppendiceCommand);
        }

        [Route("api/appendices/update")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiAppendices)]
        public async Task<int> Update([FromBody]UpdateAppendiceCommand updateAppendiceCommand)
        {
            updateAppendiceCommand.ModifiedBy = User.Identity.GetUserName();
            updateAppendiceCommand.ModifiedOn = DateTime.Now;
            updateAppendiceCommand.Deleted = false;
            updateAppendiceCommand.FileName = await UploadAppendices(updateAppendiceCommand);

            return await Mediator.Send(updateAppendiceCommand);
        }


        [Route("api/appendices/delete")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Delete, DocumentResources.ApiAppendices)]
        public async Task<int> Delete([FromBody]DeleteAppendiceCommand deleteAppendiceCommand)
        {
            if (deleteAppendiceCommand == null)
            {
                throw new ArgumentNullException(nameof(deleteAppendiceCommand));
            }

            return await Mediator.Send(deleteAppendiceCommand);
        }

        [Route("api/appendices/deletefile")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Delete, DocumentResources.ApiAppendices)]
        public async Task<int> DeleteFile([FromBody]DeleteAppendiceFileCommand deleteAppendiceFileCommand)
        {
            return await Mediator.Send(deleteAppendiceFileCommand);
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

        private Task<string> UploadAppendices(CreateAppendiceCommand command)
        {
            List<string> files = new List<string>();
            string folderPath = GetFolderPath(command.Code);
            if (command.Files != null && command.Files.Any())
            {
                foreach (HttpFile file in command.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    command.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    SaveAs(filePath, file);
                }
            }

            return Task.FromResult(string.Join(";", files));
        }

        private Task<string> UploadAppendices(UpdateAppendiceCommand command)
        {
            List<string> files = new List<string>();
            string folderPath = GetFolderPath(command.Code);
            if (command.Files != null && command.Files.Any())
            {
                foreach (HttpFile file in command.Files)
                {
                    string filePath = $"{folderPath}/{file.FileName}";
                    command.LinkFile = $"/downloadfile/viewfile?sourcedoc={folderPath.Replace(UploadFolderPath, string.Empty)}/{file.FileName}";
                    files.Add(file.FileName);
                    SaveAs(filePath, file);
                }
            }

            return Task.FromResult(string.Join(";", files));
        }
    }
}