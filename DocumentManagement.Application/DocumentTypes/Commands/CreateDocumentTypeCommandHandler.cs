using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.DocumentTypes.Commands
{
    public class CreateDocumentTypeCommandHandler : IRequestHandler<CreateDocumentTypeCommand, int>
    {
        private readonly DocumentDbContext _context;
        public CreateDocumentTypeCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            DocumentType documentType = request.ToDocumentType();
            _context.DocumentTypes.Add(documentType);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
