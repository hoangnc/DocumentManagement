using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Appendices.Queries
{
    public class GetAppendiceByIdQueryHandler : IRequestHandler<GetAppendiceByIdQuery, GetAppendiceByIdDto>
    {
        private readonly DocumentDbContext _context;
        public GetAppendiceByIdQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<GetAppendiceByIdDto> Handle(GetAppendiceByIdQuery request, CancellationToken cancellationToken)
        {
            GetAppendiceByIdDto getAppendiceByIdDto = await _context.Appendices
                .Where(d => !d.Deleted && d.Id == request.Id)
                .Select(d => new GetAppendiceByIdDto
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name,
                    CompanyCode = d.CompanyCode,
                    AppendiceNumber = d.AppendiceNumber,
                    DepartmentCode = d.DepartmentCode,
                    Approver = d.Approver,
                    DDCAudited = d.DDCAudited,
                    DocumentType = d.DocumentType,
                    EffectiveDate = d.EffectiveDate,
                    FileName = d.FileName,
                    Module = d.Module,
                    ReviewDate = d.ReviewDate,
                    ReviewNumber = d.ReviewNumber,
                    ScopeOfApplication = d.ScopeOfApplication,
                    ScopeOfDeloyment = d.ScopeOfDeloyment,
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
                                                       ReviewNumber = d1.ReviewNumber,
                                                       EffectiveDate = d1.EffectiveDate,
                                                       ReviewDate = d1.ReviewDate
                                                   }).ToList(),
                })
                .FirstOrDefaultAsync();

            return getAppendiceByIdDto;
        }
    }
}
