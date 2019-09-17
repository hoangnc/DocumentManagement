using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Text;
using MediatR;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DocumentManagement.Application.Appendices.Commands
{
    public class DeleteAppendiceFileCommandHandler : IRequestHandler<DeleteAppendiceFileCommand, int>
    {
        private readonly DocumentDbContext _context;
        private readonly string UploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];
        public DeleteAppendiceFileCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteAppendiceFileCommand request, CancellationToken cancellationToken)
        {
            Appendice appendice = await _context.Appendices.FirstOrDefaultAsync(a=>!a.Deleted && a.Id == request.Id);

            if (!appendice.LinkFile.IsNullOrEmpty())
            {
                NameValueCollection query = HttpUtility.ParseQueryString(appendice.LinkFile);
                string filePath = $"{UploadFolderPath}/{query.Get("sourcedoc")}";
                if (!filePath.IsNullOrEmpty())
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }

            appendice.FileName = string.Empty;
            appendice.LinkFile = string.Empty;
            return await _context.SaveChangesAsync();
        }
    }
}
