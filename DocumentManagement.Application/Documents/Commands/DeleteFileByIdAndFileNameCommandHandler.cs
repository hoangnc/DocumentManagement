using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Commands
{
    public class DeleteFileByIdAndFileNameCommandHandler : IRequestHandler<DeleteFileByIdAndFileNameCommand, int>
    {
        private readonly DocumentDbContext _context;

        public DeleteFileByIdAndFileNameCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteFileByIdAndFileNameCommand request, CancellationToken cancellationToken)
        {
            Document document = await _context.Documents.FirstOrDefaultAsync(a => a.Id == request.Id
            && !a.Deleted);

            if (document != null && document.Id > 0)
            {
                if (!string.IsNullOrEmpty(document.FileName))
                {
                    List<string> files = document.FileName.Split(';').ToList();
                    files.Remove(request.FileName);

                    document.FileName = string.Join(";", files);
                    document.LinkFile = string.Empty;
                    return await _context.SaveChangesAsync();
                }
            }
            return -1;
        }
    }
}
