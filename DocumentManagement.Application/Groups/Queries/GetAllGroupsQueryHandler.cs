using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Groups.Queries
{
    public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, List<GetAllGroupsDto>>
    {
        private readonly DocumentDbContext _context;
        public GetAllGroupsQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllGroupsDto>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            List<Group> groups = await _context.Groups.ToListAsync(cancellationToken);
            List<GetAllGroupsDto> result = groups.Select(group => group.ToGetAllGroupsDto()).ToList();
            return result;
        }
    }
}
