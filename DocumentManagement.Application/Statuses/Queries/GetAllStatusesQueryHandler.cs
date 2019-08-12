using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Statuses.Queries
{
    public class GetAllStatusesQueryHandler : IRequestHandler<GetAllStatusesQuery, List<GetAllStatusesDto>>
    {
        private readonly DocumentDbContext _context;
        public GetAllStatusesQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllStatusesDto>> Handle(GetAllStatusesQuery request, CancellationToken cancellationToken)
        {
            List<Status> statuses = await _context.Statuses
                .Where(m => !m.Deleted).ToListAsync();
            List<GetAllStatusesDto> getAllStatusesDtos = statuses.Select(p => p.ToGetAllStatusesDto()).ToList();
            return getAllStatusesDtos;
        }
    }
}
