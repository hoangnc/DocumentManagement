using DocumentManagement.Application.Statuses.Queries;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class StatusesApiController : BaseApiController
    {
        [Route("api/statuses/getallstatuses")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiPromulgateStatuses)]
        public async Task<List<GetAllStatusesDto>> GetAllStatuses()
        {
            return await Mediator.Send(new GetAllStatusesQuery());
        }
    }
}
