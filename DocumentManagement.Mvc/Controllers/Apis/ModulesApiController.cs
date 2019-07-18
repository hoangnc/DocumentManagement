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
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers.Apis
{
    [Authorize]
    public class ModulesApiController : BaseApiController
    {

        [Route("api/modules/create")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocuments)]
        public async Task<int> Create([FromBody]CreateModuleCommand createModuleCommand)
        {
            createModuleCommand.CreatedBy = User.Identity.Name;
            createModuleCommand.CreatedOn = DateTime.Now;
            createModuleCommand.Deleted = false;
            
            return await Mediator.Send(createModuleCommand);
        }

        [Route("api/modules/searchmodulesbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocuments)]
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
