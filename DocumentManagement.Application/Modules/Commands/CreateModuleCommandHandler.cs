using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Modules.Commands
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, int>
    {
        private readonly DocumentDbContext _context;
        public CreateModuleCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            Module module = request.ToModule();
            _context.Modules.Add(module);
            return await _context.SaveChangesAsync();
        }
    }
}
