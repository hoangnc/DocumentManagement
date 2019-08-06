using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.PromulgateStatuses.Commands
{
    public class UpdatePromulgateStatusCommandHandler : IRequestHandler<UpdatePromulgateStatusCommand, int>
    {
        private readonly DocumentDbContext _context;
        public UpdatePromulgateStatusCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdatePromulgateStatusCommand request, CancellationToken cancellationToken)
        {
            PromulgateStatus promulgateStatus = await _context.PromulgateStatuses.FirstOrDefaultAsync(d => !d.Deleted
            && d.Id == request.Id);

            if (promulgateStatus == null)
            {
                throw new NotFoundException(nameof(PromulgateStatus), request.Id);
            }

            promulgateStatus.Code = request.Code;
            promulgateStatus.Name = request.Name;
            promulgateStatus.ModifiedBy = request.ModifiedBy;
            promulgateStatus.ModifiedOn = DateTime.Now;

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
