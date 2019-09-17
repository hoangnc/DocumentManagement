using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Text;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            Document document = _context.Documents.Where(a => a.Id == request.Id
                                                                        && !a.Deleted)
                .FirstOrDefault();
            if (document != null && document.Id > 0)
            {
                if (!string.IsNullOrEmpty(document.FileName))
                {
                    List<string> oldFiles = document.FileName.Split(';').Where(f=> !f.IsNullOrEmpty()).ToList();
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
                document.ContentChange = request.ContentChange;
                document.DocumentType = request.DocumentType;
                document.EffectiveDate = request.EffectiveDate;
                document.RelateToDocuments = request.RelateToDocuments;
                document.ScopeOfApplication = request.ScopeOfApplication;
                document.ScopeOfDeloyment = request.ScopeOfDeloyment;
                document.FormType = request.FormType;
                document.DDCAudited = request.DDCAudited;
                document.ReviewDate = request.ReviewDate;
                document.LinkFile = request.LinkFile;
                document.FolderName = request.FolderName;
                document.Name = request.Name;

                if (request.Appendices !=null && request.Appendices.Any())
                {
                    List<Appendice> appendices = request.Appendices.Select(a => new Appendice
                    {
                        Id = a.Id,
                        Active = document.Active,
                        Code = a.Code?? $"PL{DateTime.Now.ToString("yyyymmddtthhss")}",
                        DDCAudited = document.DDCAudited,
                        AppendiceNumber = document.DocumentNumber,
                        ReviewNumber = a.ReviewNumber,
                        LinkFile = a.LinkFile,
                        Approver = document.Approver,
                        Name = a.Name,
                        FileName = a.FileName,
                        CompanyCode = document.CompanyCode,
                        CompanyName = document.CompanyName,
                        DepartmentCode = document.DepartmentCode,
                        DepartmentName = document.DepartmentName,
                        CreatedBy = document.CreatedBy,
                        CreatedOn = document.CreatedOn,
                        Deleted = false,
                        DocumentType = "PL",
                        EffectiveDate = document.EffectiveDate,
                        Module = document.Module,
                        StatusId = document.StatusId,
                        FormType = document.FormType,
                        PromulgateStatusId = document.PromulgateStatusId,
                        RelateToDocuments = document.Code,
                        DocumentId = document.Id,
                        ScopeOfApplication = document.ScopeOfApplication,
                        ScopeOfDeloyment = document.ScopeOfDeloyment
                    }).ToList();

                    _context.Appendices.AddOrUpdate(a=> new {a.Id }, appendices.ToArray());
                }
                await _context.SaveChangesAsync();
            }
            return document.Id;
        }
    }
}
