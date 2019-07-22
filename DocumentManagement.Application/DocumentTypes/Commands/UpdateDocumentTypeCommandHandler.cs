using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Exceptions;
using MediatR;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.DocumentTypes.Commands
{
    public class UpdateDocumentTypeCommandHandler : IRequestHandler<UpdateDocumentTypeCommand, int>
    {
        private readonly DocumentDbContext _context;
        public UpdateDocumentTypeCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            DocumentType documentType = await _context.DocumentTypes.FirstOrDefaultAsync(d => !d.Deleted
            && d.Id == request.Id);

            if(documentType == null)
            {
                throw new NotFoundException(nameof(DocumentType), request.Id);
            }

            documentType.Code = request.Code;
            documentType.Name = request.Name;
            documentType.ModifiedBy = request.ModifiedBy;
            documentType.ModifiedOn = DateTime.Now;

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
