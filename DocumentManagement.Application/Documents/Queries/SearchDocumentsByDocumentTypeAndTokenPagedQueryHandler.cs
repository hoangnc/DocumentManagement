using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Data;
using DT.Core.Data.Models;
using DT.Core.Data.Paged;
using DT.Core.Text;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Documents.Queries
{
    public class SearchDocumentsByDocumentTypeAndTokenPagedQueryHandler : SearchDocumentsQueryBaseHandler, IRequestHandler<SearchDocumentsByDocumentTypeAndTokenPagedQuery, DataSourceResult>
    {
        private readonly DocumentDbContext _context;
        public SearchDocumentsByDocumentTypeAndTokenPagedQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchDocumentsByDocumentTypeAndTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Document> query = _context.Documents.AsQueryable();

            query = query.Where(d => !d.Deleted && d.DocumentType == request.DocumentType
            && (_context.StringSplit(d.ScopeOfDeloyment, ";").Any(a1 => a1.SplitData == request.Department)
            || d.Approver == request.UserName 
            || d.Auditor == request.UserName 
            || d.Drafter == request.UserName
            || d.CreatedBy == request.UserName));

            request.Token = request.Token?.ToLowerInvariant()?.NonUnicode();

            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                if (request.AdvancedSearch)
                {
                    string fileNames = string.Join(";", FindFilesByToken(request.Token));

                    query = query.Where(c => _context.NonUnicode(c.Name).Contains(request.Token)
                            || _context.NonUnicode(c.ContentChange).Contains(request.Token)
                            || _context.NonUnicode(c.DepartmentName).Contains(request.Token)
                            || _context.NonUnicode(c.DocumentNumber).Contains(request.Token)
                            || _context.NonUnicode(c.CompanyName).Contains(request.Token)
                            || _context.NonUnicode(c.FileName).Contains(request.Token)
                            || _context.NonUnicode(c.FolderName).Contains(request.Token)
                            || _context.NonUnicode(c.LinkFile).Contains(request.Token)
                            || _context.NonUnicode(c.Module).Contains(request.Token)
                            || _context.NonUnicode(c.RelateToDocuments).Contains(request.Token)
                            || _context.NonUnicode(c.ReplaceOf).Contains(request.Token)
                            || _context.NonUnicode(c.ReviewNumber).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfApplication).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfDeloyment).Contains(request.Token)
                            || _context.NonUnicode(c.Description).Contains(request.Token));
                            //|| _context.CompareTwoFiles(_context.NonUnicode(c.FileName), fileNames, ";"));
                }
                else
                {
                    query = query.Where(c => _context.NonUnicode(c.Name).Contains(request.Token)
                            || _context.NonUnicode(c.ContentChange).Contains(request.Token)
                            || _context.NonUnicode(c.DepartmentName).Contains(request.Token)
                            || _context.NonUnicode(c.DocumentNumber).Contains(request.Token)
                            || _context.NonUnicode(c.CompanyName).Contains(request.Token)
                            || _context.NonUnicode(c.FileName).Contains(request.Token)
                            || _context.NonUnicode(c.FolderName).Contains(request.Token)
                            || _context.NonUnicode(c.LinkFile).Contains(request.Token)
                            || _context.NonUnicode(c.Module).Contains(request.Token)
                            || _context.NonUnicode(c.RelateToDocuments).Contains(request.Token)
                            || _context.NonUnicode(c.ReplaceOf).Contains(request.Token)
                            || _context.NonUnicode(c.ReviewNumber).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfApplication).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfDeloyment).Contains(request.Token)
                            || _context.NonUnicode(c.Description).Contains(request.Token));
                }
            }

            if (!request.DataSourceRequest.SortDataField.IsNullOrEmpty())
            {
                if (QueryHelper.PropertyExists<Document>(request.DataSourceRequest.SortDataField))
                {
                    switch (request.DataSourceRequest.SortOrder)
                    {
                        case "asc":
                            query = QueryHelper.OrderByProperty(query, request.DataSourceRequest.SortDataField);
                            break;
                        case "desc":
                            query = QueryHelper.OrderByPropertyDescending(query, request.DataSourceRequest.SortDataField);
                            break;
                        default:
                            query = query.OrderByDescending(u => u.CreatedOn);
                            break;
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(u => u.CreatedOn);
            }

            IQueryable<SearchDocumentsByDocumentTypeAndTokenPagedDto> test = query.Select(document => new SearchDocumentsByDocumentTypeAndTokenPagedDto
            {
                Id = document.Id,
                Approver = document.Approver,
                Auditor = document.Auditor,
                Code = document.Code,
                CompanyCode = document.CompanyCode,
                CompanyName = document.CompanyName,
                ContentChange = document.ContentChange,
                DDCAudited = document.DDCAudited,
                DepartmentCode = document.DepartmentCode,
                DepartmentName = document.DepartmentName,
                Description = document.Description,
                DocumentNumber = document.DocumentNumber,
                DocumentType = document.DocumentType,
                Drafter = document.Drafter,
                EffectiveDate = document.EffectiveDate,
                FileName = document.FileName,
                FolderName = document.FolderName,
                LinkFile = document.LinkFile,
                Module = document.Module,
                Name = document.Name,
                ReplaceEffectiveDate = document.ReplaceEffectiveDate,
                ReplaceOf = document.ReplaceOf,
                RelateToDocuments = document.RelateToDocuments,
                ListReplaceOf = _context.Documents.
                Where(d => _context.StringSplit(document.ReplaceOf, ";").Any(a1 => a1.SplitData == d.Code))
                .Select(d => d.Name)
                .ToList(),
                ListRelateToDocuments = _context.Documents.
                Where(d => _context.StringSplit(document.RelateToDocuments, ";").Any(a1 => a1.SplitData == d.Code))
                .Select(d => d.Name)
                .ToList(),
                ReviewDate = document.ReviewDate,
                ReviewNumber = document.ReviewNumber,
                ScopeOfApplication = document.ScopeOfApplication,
                ScopeOfDeloyment = document.ScopeOfDeloyment
            });

            PagedList<SearchDocumentsByDocumentTypeAndTokenPagedDto> queryResult = new PagedList<SearchDocumentsByDocumentTypeAndTokenPagedDto>();
            await queryResult.CreateAsync(test, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);

            return new DataSourceResult
            {
                Data = queryResult.ToList(),
                Total = queryResult.TotalCount
            };
        }
    }
}
