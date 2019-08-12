using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Data;
using DT.Core.Data.Models;
using DT.Core.Data.Paged;
using DT.Core.Helper;
using DT.Core.Text;
using MediatR;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xceed.Words.NET;

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
            query = query.Where(c => !c.Deleted && c.DocumentType == request.DocumentType);

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
                            || _context.NonUnicode(c.Description).Contains(request.Token)
                            || _context.CompareTwoFiles(_context.NonUnicode(c.FileName), fileNames, ";"));
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

            PagedList<Document> queryResult = new PagedList<Document>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchDocumentsByDocumentTypeAndTokenPagedDto> data = queryResult.Select(u => u.ToSearchDocumentsByDocumentTypeAndTokenPagedDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
