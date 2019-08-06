using DocumentManagement.Application.DocumentTypes.Commands;
using DocumentManagement.Application.DocumentTypes.Queries;
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
    public class DocumentTypesApiController : BaseApiController
    {
        [Route("api/documenttypes/create")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.ApiDocumentTypes)]
        public async Task<int> Create([FromBody]CreateDocumentTypeCommand createDocumentTypeCommand)
        {
            createDocumentTypeCommand.CreatedBy = User.Identity.GetUserName();
            createDocumentTypeCommand.CreatedOn = DateTime.Now;
            createDocumentTypeCommand.Deleted = false;

            return await Mediator.Send(createDocumentTypeCommand);
        }

        [Route("api/documenttypes/update")]
        [HttpPost]
        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.ApiDocumentTypes)]
        public async Task<int> Update([FromBody]UpdateDocumentTypeCommand updateDocumentTypeCommand)
        {
            updateDocumentTypeCommand.ModifiedBy = User.Identity.GetUserName();
            updateDocumentTypeCommand.ModifiedOn = DateTime.Now;

            return await Mediator.Send(updateDocumentTypeCommand);
        }

        [Route("api/documenttypes/getalldocumenttypes")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocumentTypes)]
        public async Task<List<GetAllDocumentTypesDto>> List()
        {
            return await Mediator.Send(new GetAllDocumentTypesQuery());
        }

        [Route("api/documenttypes/searchdocumenttypesbytokenpaged")]
        [HttpGet]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.ApiDocumentTypes)]
        public async Task<DataSourceResult> List([FromUri]DataSourceRequest dataSourceRequest, string token)
        {
            return await Mediator.Send(new SearchDocumentTypesByTokenPagedQuery
            {
                DataSourceRequest = dataSourceRequest,
                Token = token
            });
        }
    }
}
