using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Appendices.Commands
{
    public class CreateAppendiceCommandHandler : IRequestHandler<CreateAppendiceCommand, int>
    {
        private readonly DocumentDbContext _context;
        public CreateAppendiceCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateAppendiceCommand request, CancellationToken cancellationToken)
        {
            Appendice appendice = request.ToAppendice();
            _context.Appendices.Add(appendice);
            return await _context.SaveChangesAsync();
        }
    }
}
