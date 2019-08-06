using DocumentManagement.Application.Appendices.Commands;
using DocumentManagement.Application.Appendices.Queries;
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
    public class AppendicesApiController : BaseApiController
    {
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

        private Task<string> UploadAppendices(CreateAppendiceCommand command)
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

        /*private Task<string> UploadDocuments(UpdateDocumentCommand command)
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
        }*/
    }
}