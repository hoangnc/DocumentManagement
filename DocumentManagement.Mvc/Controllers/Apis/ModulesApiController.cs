using DocumentManagement.Application.Modules.Commands;
using DocumentManagement.Application.Modules.Queries;
using DT.Core.Data.Models;
using DT.Core.Web.Common.Api.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using DT.Core.Web.Common.Identity.Extensions;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class ModulesApiController : BaseApiController
    {
        [Route("api/modules/create")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiModules)]
        public async Task<int> Create([FromBody]CreateModuleCommand createModuleCommand)
        {
            createModuleCommand.CreatedBy = User.Identity.GetUserName();
            createModuleCommand.CreatedOn = DateTime.Now;
            createModuleCommand.Deleted = false;
            
            return await Mediator.Send(createModuleCommand);
        }

        [Route("api/modules/update")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.ApiModules)]
        public async Task<int> Update([FromBody]UpdateModuleCommand updateModuleCommand)
        {
            updateModuleCommand.ModifiedBy = User.Identity.GetUserName();
            updateModuleCommand.ModifiedOn = DateTime.Now;

            return await Mediator.Send(updateModuleCommand);
        }

        [Route("api/modules/getallmodules")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiModules)]
        public async Task<List<GetAllModulesDto>> GetAllModules()
        {
            return await Mediator.Send(new GetAllModulesQuery());
        }

        [Route("api/modules/searchmodulesbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiModules)]
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            return await Mediator.Send(new SearchModulesByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });
        }
    }
}
