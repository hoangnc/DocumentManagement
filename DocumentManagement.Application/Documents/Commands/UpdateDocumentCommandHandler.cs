using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Text;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Commands
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, int>
    {
        private readonly DocumentDbContext _context;

        public UpdateDocumentCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            Document document = await _context.Documents.FirstOrDefaultAsync(a => a.Id == request.Id
                                                                                && !a.Deleted);
            if (document != null && document.Id > 0)
            {
                if (!string.IsNullOrEmpty(document.FileName))
                {
                    List<string> oldFiles = document.FileName.Split(';').Where(f=>!f.IsNullOrEmpty()).ToList();
                    List<string> newFiles = request.FileName.Split(';').Where(f => !f.IsNullOrEmpty()).ToList();
                    foreach (string newFile in newFiles)
                    {
                        if (!oldFiles.Any(file => file == newFile))
                        {
                            oldFiles.Add(newFile);
                        }
                    }
                    document.FileName = string.Join(";", oldFiles);
                }
                else
                {
                    document.FileName = request.FileName;
                }

                document.ModifiedBy = request.ModifiedBy;
                document.ModifiedOn = request.ModifiedOn;
                document.Approver = request.Approver;
                document.Drafter = request.Drafter;
                document.Auditor = request.Auditor;
                document.CompanyCode = request.CompanyCode;
                document.CompanyName = request.CompanyName;
                document.Module = request.Module;
                document.DepartmentCode = request.DepartmentCode;
                document.DepartmentName = request.DepartmentName;
                document.Description = request.Description;
                document.DocumentNumber = request.DocumentNumber;
                document.DocumentType = request.DocumentType;
                document.EffectiveDate = request.EffectiveDate;
                document.RelateToDocuments = request.RelateToDocuments;
                document.ScopeOfApplication = request.ScopeOfApplication;
                document.ScopeOfDeloyment = request.ScopeOfDeloyment;
                document.FormType = request.FormType;

                await _context.SaveChangesAsync();
            }
            return document.Id;
        }
    }
}
