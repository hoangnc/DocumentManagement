using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetAllDocumentQueryHandler : IRequestHandler<GetAllDocumentQuery, List<GetAllDocumentDto>>
    {
        private readonly DocumentDbContext _context;
        public GetAllDocumentQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllDocumentDto>> Handle(GetAllDocumentQuery request, CancellationToken cancellationToken)
        {
            List<Document> documents = await _context.Documents.ToListAsync();
            var result = documents.Select(documentType => documentType.ToGetAllDocumentDto()).ToList();
            return result;
        }
    }
}
