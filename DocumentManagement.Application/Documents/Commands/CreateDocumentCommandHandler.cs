using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Commands
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
    {
        private readonly DocumentDbContext _context;
        public CreateDocumentCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            Document document = request.ToDocument();
            document.FolderName = document.DocumentType;
            _context.Documents.Add(document);
            return await _context.SaveChangesAsync();
        }
    }
}
