using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Commands
{
    public class ReviewDocumentCommandHandler : IRequestHandler<ReviewDocumentCommand, int>
    {
        private readonly DocumentDbContext _context;
        public ReviewDocumentCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(ReviewDocumentCommand request, CancellationToken cancellationToken)
        {
            Document document = request.ToDocument();
            document.FolderName = document.DocumentType;
            _context.Documents.Add(document);
            return await _context.SaveChangesAsync();
        }
    }
}
