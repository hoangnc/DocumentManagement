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
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest)
        {
            return await Mediator.Send(new SearchDocumentsByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest
            });
        }
    }
}