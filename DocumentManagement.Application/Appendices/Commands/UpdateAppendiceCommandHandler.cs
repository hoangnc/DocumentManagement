using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Appendices.Commands
{
    public class UpdateAppendiceCommandHandler : IRequestHandler<UpdateAppendiceCommand, int>
    {
        private readonly DocumentDbContext _context;

        public UpdateAppendiceCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateAppendiceCommand request, CancellationToken cancellationToken)
        {
            Appendice appendice = await _context.Appendices.Where(a => a.Id == request.Id
                                                                  && !a.Deleted).FirstOrDefaultAsync();

            if(appendice != null && appendice.Id > 0)
            {
                appendice.Approver = request.Approver;
                appendice.CompanyCode = request.CompanyCode;
                appendice.CompanyName = request.CompanyName;
                appendice.DDCAudited = request.DDCAudited;
                appendice.DepartmentCode = request.DepartmentCode;
                appendice.DepartmentName = request.DepartmentName;
                appendice.EffectiveDate = request.EffectiveDate;
                appendice.AppendiceNumber = request.AppendiceNumber;
 
                if (request.Files != null && request.Files.Any())
                {
                    appendice.FileName = request.FileName;
                    appendice.LinkFile = request.LinkFile;
                }
                appendice.ModifiedBy = request.ModifiedBy;
                appendice.ModifiedOn = request.ModifiedOn;
                appendice.Module = request.Module;
                appendice.Name = request.Name;
                appendice.RelateToDocuments = request.RelateToDocuments;
                appendice.ReviewDate = request.ReviewDate;
                appendice.ReviewNumber = request.ReviewNumber;
                appendice.ScopeOfApplication = request.ScopeOfApplication;
                appendice.ScopeOfDeloyment = request.ScopeOfDeloyment;

                return await _context.SaveChangesAsync();
            }

            return appendice.Id;
        }
    }
}
