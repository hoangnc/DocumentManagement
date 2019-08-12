using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.PromulgateStatuses.Queries
{
    public class GetAllPromulgateStatusesQuery : IRequest<List<GetAllPromulgateStatusesDto>>
    {
    }
}
