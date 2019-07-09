using DocumentManagement.Application.DocumentTypes.Queries;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class DocumentTypesApiController : BaseApiController
    {
        [Route("api/documenttypes/getalldocumenttypes")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocuments)]
        public async Task<List<GetAllDocumentTypesDto>> List()
        {
            return await Mediator.Send(new GetAllDocumentTypesQuery());
        }
    }
}
