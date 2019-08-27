using DocumentManagement.Persistence;
using MediatR;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetDocumentByCodeQueryHandler : IRequestHandler<GetDocumentByCodeQuery, GetDocumentByCodeDto>
    {
        private readonly DocumentDbContext _context;
        public GetDocumentByCodeQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<GetDocumentByCodeDto> Handle(GetDocumentByCodeQuery request, CancellationToken cancellationToken)
        {
            GetDocumentByCodeDto getDocumentByCodeDto = await _context.Documents
                .Where(d => !d.Deleted && d.Code == request.Code)
                .Select(d => new GetDocumentByCodeDto
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
                    Active = d.Active,
                    ContentChange = d.ContentChange,
                    DocumentNumber = d.DocumentNumber,
                    Drafter = d.Drafter,
                    DocumentType = d.DocumentType,
                    EffectiveDate = d.EffectiveDate,
                    FileName = d.FileName,
                    FolderName = d.FolderName,
                    Module = d.Module,
                    ReviewDate = d.ReviewDate,
                    ReviewNumber = d.ReviewNumber,
                    ScopeOfApplication = d.ScopeOfApplication,
                    ScopeOfDeloyment = d.ScopeOfDeloyment,
                    ReplaceByDocuments = _context.Documents.Where(d1=>_context.StringSplit(d1.RelateToDocuments, ";").Any(d2=>d2.SplitData == d.Code))
                    .Select(d1=>new ReplaceToDocumentDto {
                        Code = d1.Code,
                        Name = d1.Name,
                        FileName = d1.FileName,
                        FolderName = d1.FolderName,
                        DocumentNumber = d1.DocumentNumber,
                        EffectiveDate = d1.EffectiveDate,
                        ReviewDate = d1.ReviewDate
                    }).ToList(),
                    RelateToDocuments = _context.Documents.Join(
                                                   _context.StringSplit(d.RelateToDocuments, ";"),
                                                   d1 => d1.Code,
                                                   d2 => d2.SplitData,
                                                   (d1, d2) => new RelateToDocumentDto
                                                   {
                                                       Code = d1.Code,
                                                       Name = d1.Name,
                                                       FileName = d1.FileName,
                                                       FolderName = d1.FolderName,
                                                       DocumentNumber = d1.DocumentNumber,
                                                       EffectiveDate = d1.EffectiveDate,
                                                       ReviewDate = d1.ReviewDate
                                                   }).ToList(),
                    ReplaceToDocuments = _context.Documents.Join(_context.StringSplit(d.ReplaceOf, ";"),
                                                   d1 => d1.Code,
                                                   d2 => d2.SplitData,
                                                   (d1, d2) => new ReplaceToDocumentDto
                                                   {
                                                       Code = d1.Code,
                                                       Name = d1.Name,
                                                       FileName = d1.FileName,
                                                       FolderName = d1.FolderName,
                                                       DocumentNumber = d1.DocumentNumber,
                                                       EffectiveDate = d1.EffectiveDate,
                                                       ReviewDate = d1.ReviewDate
                                                   }).ToList(),
                    Appendices = _context.Appendices.Where(a => _context.StringSplit(a.RelateToDocuments, ";").Any(a1=>a1.SplitData == d.Code))
                                                    .Select(a => new AppendiceDto
                                                    {
                                                        Code = a.Code,
                                                        Name = a.Name,
                                                        DocumentType = a.DocumentType,
                                                        FileName = a.FileName,
                                                        DocumentNumber = a.AppendiceNumber,
                                                        ReviewNumber = a.ReviewNumber,
                                                        LinkFile = a.LinkFile
                                                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return getDocumentByCodeDto;
        }
    }
}
