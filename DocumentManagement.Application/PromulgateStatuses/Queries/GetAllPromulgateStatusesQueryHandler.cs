using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.PromulgateStatuses.Queries
{
    public class GetAllPromulgateStatusesQueryHandler : IRequestHandler<GetAllPromulgateStatusesQuery, List<GetAllPromulgateStatusesDto>>
    {
        private readonly DocumentDbContext _context;
        public GetAllPromulgateStatusesQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllPromulgateStatusesDto>> Handle(GetAllPromulgateStatusesQuery request, CancellationToken cancellationToken)
        {
            List<PromulgateStatus> promulgateStatuses = await _context.PromulgateStatuses
                .Where(m => !m.Deleted)
                .ToListAsync();
            List<GetAllPromulgateStatusesDto> getAllPromulgateStatusesDtos = promulgateStatuses.Select(p => p.ToGetAllPromulgateStatusesDto()).ToList();
            return getAllPromulgateStatusesDtos;
        }
    }
}
