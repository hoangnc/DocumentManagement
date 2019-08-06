using DocumentManagement.Application.Mapper;
using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Data;
using DT.Core.Data.Models;
using DT.Core.Data.Paged;
using DT.Core.Text;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Appendices.Queries
{
    public class SearchAppendicesByTokenPagedQueryHandler : SearchDocumentsQueryBaseHandler, IRequestHandler<SearchAppendicesByTokenPagedQuery, DataSourceResult>
    {
        private readonly DocumentDbContext _context;
        public SearchAppendicesByTokenPagedQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchAppendicesByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Appendice> query = _context.Appendices.AsQueryable();
            query = query.Where(c => !c.Deleted);

            request.Token = request.Token?.ToLowerInvariant()?.NonUnicode();

            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                if (request.AdvancedSearch)
                {
                    string fileNames = string.Join(";", FindFilesByToken(request.Token));

                    query = query.Where(c => _context.NonUnicode(c.Name).Contains(request.Token)
                            || _context.NonUnicode(c.DepartmentName).Contains(request.Token)
                            || _context.NonUnicode(c.CompanyName).Contains(request.Token)
                            || _context.NonUnicode(c.FileName).Contains(request.Token)
                            || _context.NonUnicode(c.LinkFile).Contains(request.Token)
                            || _context.NonUnicode(c.Module).Contains(request.Token)
                            || _context.NonUnicode(c.RelateToDocuments).Contains(request.Token)
                            || _context.NonUnicode(c.ReplaceOf).Contains(request.Token)
                            || _context.NonUnicode(c.ReviewNumber).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfApplication).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfDeloyment).Contains(request.Token)
                            || _context.CompareTwoFiles(_context.NonUnicode(c.FileName), fileNames, ";"));
                }
                else
                {
                    query = query.Where(c => _context.NonUnicode(c.Name).Contains(request.Token)
                            || _context.NonUnicode(c.DepartmentName).Contains(request.Token)
                            || _context.NonUnicode(c.CompanyName).Contains(request.Token)
                            || _context.NonUnicode(c.FileName).Contains(request.Token)
                            || _context.NonUnicode(c.LinkFile).Contains(request.Token)
                            || _context.NonUnicode(c.Module).Contains(request.Token)
                            || _context.NonUnicode(c.RelateToDocuments).Contains(request.Token)
                            || _context.NonUnicode(c.ReplaceOf).Contains(request.Token)
                            || _context.NonUnicode(c.ReviewNumber).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfApplication).Contains(request.Token)
                            || _context.NonUnicode(c.ScopeOfDeloyment).Contains(request.Token));
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

            PagedList<Appendice> queryResult = new PagedList<Appendice>();
            await queryResult.CreateAsync(query, request.DataSourceRequest.PageNum, request.DataSourceRequest.PageSize);
            List<SearchAppendicesByTokenPagedDto> data = queryResult.Select(u => u.ToSearchAppendicesByTokenPagedDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
