using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.PromulgateStatuses.Queries
{
    public class GetPromulgateStatusByIdQueryHandler : IRequestHandler<GetPromulgateStatusByIdQuery, GetPromulgateStatusByIdDto>
    {
        private readonly DocumentDbContext _context;
        public GetPromulgateStatusByIdQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<GetPromulgateStatusByIdDto> Handle(GetPromulgateStatusByIdQuery request, CancellationToken cancellationToken)
        {
            GetPromulgateStatusByIdDto getPromulgateStatusByIdDto = new GetPromulgateStatusByIdDto();
            PromulgateStatus promulgateStatus = await _context.PromulgateStatuses
                .FirstOrDefaultAsync(m => !m.Deleted
            && m.Id == request.Id);
            if (promulgateStatus != null)
            {
                getPromulgateStatusByIdDto = promulgateStatus.ToGetPromulgateStatusByIdDto();
            }
            return getPromulgateStatusByIdDto;
        }
    }
}
