using DocumentManagement.Application.UserGroups.Queries;
using DocumentManagement.Mvc.Constants;
using DT.Core.Data.Models;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using LazyCache;
using System.Security.Claims;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class UserGroupsApiController : DocumentManagementApiControllerBase
    {
        [Route("api/usergroups/SearchUserGroupsByTokenPaged")]
        [HttpGet]
        public async Task<DataSourceResult> SearchUserGroupsByTokenPaged([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string accessToken = user.FindFirst("access_token").Value;
            Func<Task<IEnumerable<dynamic>>> users = async () => await CallApi(new Uri($"{MasterDataEndpoint}/api/v1/masterdatas/getallusers"), accessToken);
            IEnumerable<dynamic> result = await AppCache.GetOrAddAsync(CacheKeys.UserCacheKey, users);

            return await Mediator.Send(new SearchUserGroupsByTokenPagedQuery {
                DataSourceRequest = dataSourceRequest,
                Token = token,
                Users = result
            });
        }
    }
}