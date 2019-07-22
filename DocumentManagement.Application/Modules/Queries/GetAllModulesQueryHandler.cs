using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Modules.Queries
{
    public class GetAllModulesQueryHandler : IRequestHandler<GetAllModulesQuery, List<GetAllModulesDto>>
    {
        private readonly DocumentDbContext _context;
        public GetAllModulesQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllModulesDto>> Handle(GetAllModulesQuery request, CancellationToken cancellationToken)
        {
            List<Module> documentTypes = await _context.Modules.ToListAsync();
            List<GetAllModulesDto> result = documentTypes.Select(documentType => documentType.ToGetAllModulesDto()).ToList();
            return result;
        }
    }
}
