using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.PromulgateStatuses.Commands
{
    public class CreatePromulgateStatusCommandHandler : IRequestHandler<CreatePromulgateStatusCommand, int>
    {
        private readonly DocumentDbContext _context;
        public CreatePromulgateStatusCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreatePromulgateStatusCommand request, CancellationToken cancellationToken)
        {
            PromulgateStatus promulgateStatus = request.ToPromulgateStatus();
            _context.PromulgateStatuses.Add(promulgateStatus);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
