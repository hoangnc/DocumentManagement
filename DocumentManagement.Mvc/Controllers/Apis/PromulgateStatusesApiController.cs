using DocumentManagement.Application.PromulgateStatuses.Commands;
using DocumentManagement.Application.PromulgateStatuses.Queries;
using DT.Core.Data.Models;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using DT.Core.Web.Common.Identity.Extensions;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class PromulgateStatusesApiController : BaseApiController
    {
        [Route("api/PromulgateStatuses/create")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiPromulgateStatuses)]
        public async Task<int> Create([FromBody]CreatePromulgateStatusCommand createPromulgateStatusCommand)
        {
            createPromulgateStatusCommand.CreatedBy = User.Identity.GetUserName();
            createPromulgateStatusCommand.CreatedOn = DateTime.Now;
            createPromulgateStatusCommand.Deleted = false;

            return await Mediator.Send(createPromulgateStatusCommand);
        }

        [Route("api/PromulgateStatuses/update")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.ApiPromulgateStatuses)]
        public async Task<int> Update([FromBody]UpdatePromulgateStatusCommand updatePromulgateStatusCommand)
        {
            updatePromulgateStatusCommand.ModifiedBy = User.Identity.GetUserName();
            updatePromulgateStatusCommand.ModifiedOn = DateTime.Now;

            return await Mediator.Send(updatePromulgateStatusCommand);
        }

        [Route("api/PromulgateStatuses/searchPromulgateStatusesbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiPromulgateStatuses)]
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            return await Mediator.Send(new SearchPromulgateStatusByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });
        }

        [Route("api/PromulgateStatuses/getallPromulgateStatuses")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiPromulgateStatuses)]
        public async Task<List<GetAllPromulgateStatusesDto>> GetAllStatuses()
        {
            return await Mediator.Send(new GetAllPromulgateStatusesQuery());
        }
    }
}