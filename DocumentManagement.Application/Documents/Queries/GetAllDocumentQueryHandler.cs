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
    public class GetAllDocumentQueryHandler : IRequestHandler<GetAllDocumentQuery, List<GetAllDocumentDto>>
    {
        private readonly DocumentDbContext _context;
        public GetAllDocumentQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllDocumentDto>> Handle(GetAllDocumentQuery request, CancellationToken cancellationToken)
        {
            List<Document> documents = await _context.Documents.Where(d => !d.Deleted).ToListAsync();
            List<GetAllDocumentDto> result = documents
                .Select(d => new GetAllDocumentDto
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name,
                    CompanyCode = d.CompanyCode,
                    DepartmentCode = d.DepartmentCode,
                    Description = d.Description,
                    Approver = d.Approver,
                    Auditor = d.Auditor,
                    DDCAudited = d.DDCAudited,
                    ContentChange = d.ContentChange,
                    DocumentNumber = d.DocumentNumber,
                    Drafter = d.Drafter,
                    DocumentType = d.DocumentType,
                    EffectiveDate = d.EffectiveDate,
                    FileName = d.FileName,
                    Module = d.Module,
                    ReviewDate = d.ReviewDate,
                    ReviewNumber = d.ReviewNumber,
                    ScopeOfApplication = d.ScopeOfApplication,
                    ScopeOfDeloyment = d.ScopeOfDeloyment,
                    Appendices = _context.Appendices.Where(a => _context.StringSplit(a.RelateToDocuments, ";").Any(a1 => a1.SplitData == d.Code))
                                                    .Select(a => new AppendiceDto
                                                    {
                                                        Id = a.Id,
                                                        LinkFile = a.LinkFile,
                                                        Code = a.Code,
                                                        Name = a.Name,
                                                        DocumentType = a.DocumentType,
                                                        FileName = a.FileName,
                                                        DocumentNumber = a.AppendiceNumber,
                                                        ReviewNumber = a.ReviewNumber
                                                    }).ToList()
                }).ToList();
            return result;
        }
    }
}
