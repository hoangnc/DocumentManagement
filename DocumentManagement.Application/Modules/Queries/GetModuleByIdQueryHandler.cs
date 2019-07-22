using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Modules.Queries
{
    public class GetModuleByIdQueryHandler : IRequestHandler<GetModuleByIdQuery, GetModuleByIdDto>
    {
        private readonly DocumentDbContext _context;
        public GetModuleByIdQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<GetModuleByIdDto> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
        {
            GetModuleByIdDto getModuleByIdDto = new GetModuleByIdDto();
            Module module = await _context.Modules.FirstOrDefaultAsync(m => !m.Deleted
            && m.Id == request.Id);
            if (module != null)
            {
                getModuleByIdDto = module.ToGetModuleByIdDto();
            }
            return getModuleByIdDto;
        }
    }
}
