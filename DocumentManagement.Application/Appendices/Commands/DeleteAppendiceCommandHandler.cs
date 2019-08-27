using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Appendices.Commands
{
    public class DeleteAppendiceCommandHandler : IRequestHandler<DeleteAppendiceCommand, int>
    {
        private readonly string UploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
        private readonly DocumentDbContext _context;
        public DeleteAppendiceCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteAppendiceCommand request, CancellationToken cancellationToken)
        {
            Appendice appendice = _context.Appendices.FirstOrDefault(a => a.Id == request.Id);
            if (appendice != null)
            {
                Document document = _context.Documents.FirstOrDefault(d => d.Id == appendice.DocumentId || d.Code == appendice.RelateToDocuments);
                if(document != null)
                {
                    try
                    {
                        string filePath = $"{UploadFolderPath}/{appendice.FileName}";
                        File.Delete(filePath);
                    }
                    catch
                    {

                    }

                }
                _context.Appendices.Remove(appendice);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }
    }
}
