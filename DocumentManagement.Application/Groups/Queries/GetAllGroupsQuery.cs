using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.Groups.Queries
{
    public class GetAllGroupsQuery : IRequest<List<GetAllGroupsDto>>
    {
    }
}
