using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.DocumentTypes.Queries
{
    public class GetAllDocumentTypesQueryHandler : IRequestHandler<GetAllDocumentTypesQuery, List<GetAllDocumentTypesDto>>
    {
        private readonly DocumentDbContext _context;
        public GetAllDocumentTypesQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllDocumentTypesDto>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
        {
            List<DocumentType> documentTypes = await _context.DocumentTypes.ToListAsync();
            var result = documentTypes.Select(documentType => documentType.ToGetAllDocumentTypesDto()).ToList();
            return result;
        }
    }
}
