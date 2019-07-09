using DocumentManagement.Application.Documents.Commands;
using DocumentManagement.Application.Documents.Queries;
using DT.Core.Data.Models;
using DT.Core.Web.Common.Api.WebApi.Controllers;
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
        public DocumentsApiController()
        {
        }

        [Route("api/documents/searchdocumentsbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocuments)]
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            return await Mediator.Send(new SearchDocumentsByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });
        }

        [Route("api/documents/create")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocuments)]
        public async Task<int> Create([FromBody]CreateDocumentCommand createDocumentCommand)
        {
            createDocumentCommand.CreatedBy = User.Identity.Name;
            createDocumentCommand.CreatedOn = DateTime.Now;
            createDocumentCommand.Deleted = false;
            createDocumentCommand.FileName = await UploadDocuments(createDocumentCommand);
            return await Mediator.Send(createDocumentCommand);
        }

        private Task<string> UploadDocuments(CreateDocumentCommand command)
        {
            List<string> files = new List<string>();
            if (command.Files.Any())
            {
                foreach (var file in command.Files)
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