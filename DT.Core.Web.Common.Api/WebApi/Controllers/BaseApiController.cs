using MediatR;
using System.Net.Http;
using System.Web.Http;

namespace DT.Core.Web.Common.Api.WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = Request.GetDependencyScope().GetService(typeof(IMediator)) as IMediator);
    }
}
