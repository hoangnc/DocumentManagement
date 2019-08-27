using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Commands
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
    {
        private readonly DocumentDbContext _context;
        public CreateDocumentCommandHandler(DocumentDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            Document existsDocument = _context.Documents.FirstOrDefault(d=> 
               d.CompanyCode == request.CompanyCode
               && d.ContentChange == request.ContentChange
               && d.DepartmentCode == request.DepartmentCode
               && d.Description == request.Description
               && d.DocumentNumber == request.DocumentNumber
               && d.DocumentType == request.DocumentType
               && d.Drafter == request.Drafter
               && d.EffectiveDate == request.EffectiveDate
               && d.FileName == request.FileName
               && d.Module == request.Module
               && d.Name == request.Name
               && d.RelateToDocuments == request.RelateToDocuments
               && d.ReplaceOf == request.ReplaceOf
               && d.ReviewDate == request.ReviewDate
               && d.ReviewNumber == request.ReviewNumber
               && d.ScopeOfApplication == request.ScopeOfApplication
               && d.ScopeOfDeloyment == request.ScopeOfDeloyment
            );

            if(existsDocument != null && existsDocument.Id > 0)
            {
                throw new ExistsException(nameof(Document), "fields");
            }

            Document document = request.ToDocument();
            _context.Documents.Add(document);
            await _context.SaveChangesAsync(cancellationToken);
            if (document.Id > 0)
            {
                if (request.Appendices != null && request.Appendices.Any())
                {
                    List<Appendice> appendices = request.Appendices.Select(a => new Appendice
                    {
                        Active = document.Active,
                        DDCAudited = document.DDCAudited,
                        AppendiceNumber = document.DocumentNumber,
                        ReviewNumber = a.ReviewNumber,
                        LinkFile = a.LinkFile,
                        Approver = document.Approver,
                        Code = $"PL{DateTime.Now.ToString("yyyymmddtthhss")}",
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
                    _context.Appendices.AddRange(appendices);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return document.Id;
        }
    }
}
