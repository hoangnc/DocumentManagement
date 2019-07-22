using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.DocumentTypes.Queries
{
    public class GetDocumentTypeByIdQueryHandler : IRequestHandler<GetDocumentTypeByIdQuery, GetDocumentTypeByIdDto>
    {
        private readonly DocumentDbContext _context;
        public GetDocumentTypeByIdQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<GetDocumentTypeByIdDto> Handle(GetDocumentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            GetDocumentTypeByIdDto getDocumentTypeByIdDto = new GetDocumentTypeByIdDto();
            DocumentType documentType = await _context.DocumentTypes
                .FirstOrDefaultAsync(m => !m.Deleted
            && m.Id == request.Id);
            if (documentType != null)
            {
                getDocumentTypeByIdDto = documentType.ToGetDocumentTypeByIdDto();
            }
            return getDocumentTypeByIdDto;
        }
    }
}
