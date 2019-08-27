using DocumentManagement.Application.Groups.Queries;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class GroupsApiController : BaseApiController
    {
        [Route("api/groups/getallgroups")]
        [HttpGet]
        // [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiModules)]
        public async Task<List<GetAllGroupsDto>> GetAllGroups()
        {
            return await Mediator.Send(new GetAllGroupsQuery());
        }
    }
}