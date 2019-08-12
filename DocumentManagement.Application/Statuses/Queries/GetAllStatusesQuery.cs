using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.Statuses.Queries
{
    public class GetAllStatusesQuery : IRequest<List<GetAllStatusesDto>>
    {
    }
}
