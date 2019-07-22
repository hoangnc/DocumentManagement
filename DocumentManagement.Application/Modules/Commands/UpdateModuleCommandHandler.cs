using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Exceptions;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Modules.Commands
{
    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, int>
    {
        private readonly DocumentDbContext _context;
        public UpdateModuleCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            Module module = await _context.Modules.FirstOrDefaultAsync(d => !d.Deleted
            && d.Id == request.Id);

            if (module == null)
            {
                throw new NotFoundException(nameof(DocumentType), request.Id);
            }

            module.Code = request.Code;
            module.Name = request.Name;
            module.ModifiedBy = request.ModifiedBy;
            module.ModifiedOn = DateTime.Now;

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
