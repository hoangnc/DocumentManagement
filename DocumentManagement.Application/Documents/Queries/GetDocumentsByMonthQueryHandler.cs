using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetDocumentsByMonthQueryHandler : IRequestHandler<GetDocumentsByMonthQuery, List<GetDocumentsByMonthDto>>
    {
        private readonly DocumentDbContext _context;
        public GetDocumentsByMonthQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetDocumentsByMonthDto>> Handle(GetDocumentsByMonthQuery request, CancellationToken cancellationToken)
        {
            List<GetDocumentsByMonthDto> documents = await _context.Documents
                .Where(d => d.CreatedOn.Year == request.Year
                && d.CreatedOn.Month == request.Month)
                .Select(d=> new GetDocumentsByMonthDto
                {
                    Name = d.Name,
                    FileName = d.FileName,
                    DocumentNumber = d.DocumentNumber,
                    EffectiveDate = d.EffectiveDate,
                    Code = d.Code,
                    DocumentType = _context.DocumentTypes.FirstOrDefault(dt => dt.Code == d.DocumentType).Name,
                    ReviewNumber = d.ReviewNumber,
                    ReplaceOf = _context.Documents.FirstOrDefault(d1=>d1.Code == d.ReplaceOf).Name,
                    ScopeOfDeloyment = d.ScopeOfDeloyment,
                    Approver = d.Approver
                })
                .ToListAsync();
            
            return documents;
        }
    }
}
