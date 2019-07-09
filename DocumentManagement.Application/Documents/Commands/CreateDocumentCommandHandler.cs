using DocumentManagement.Application.Mapper;
using DocumentManagement.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var document = request.ToDocument();
            document.FolderName = document.DocumentType;
            _context.Documents.Add(document);
            return await _context.SaveChangesAsync();
        }
    }
}
