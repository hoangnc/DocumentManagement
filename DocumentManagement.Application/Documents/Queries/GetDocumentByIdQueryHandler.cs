using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, GetDocumentByIdDto>
    {
        private readonly DocumentDbContext _context;
        public GetDocumentByIdQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<GetDocumentByIdDto> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            GetDocumentByIdDto getDocumentByIdDto = new GetDocumentByIdDto();
            Document document = await _context.Documents
                .FirstOrDefaultAsync(m => !m.Deleted
            && m.Id == request.Id);
            if (document != null)
            {
                getDocumentByIdDto = document.ToGetDocumentByIdDto();
            }
            return getDocumentByIdDto;
        }
    }
}
