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

namespace DocumentManagement.Application.Documents.Queries
{
    public class SearchDocumentsByTokenPagedQueryHandler : IRequestHandler<SearchDocumentsByTokenPagedQuery, DataSourceResult>
    {
        private readonly DocumentDbContext _context;
        public SearchDocumentsByTokenPagedQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public async Task<DataSourceResult> Handle(SearchDocumentsByTokenPagedQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Document> query = _context.Documents.AsQueryable();
            query = query.Where(c => !c.Deleted);

            if (!string.IsNullOrWhiteSpace(request.Token))
            {
                query = query.Where(c => c.Name.Contains(request.Token)
                || c.ContentChange.Contains(request.Token)
                || c.DepartmentName.Contains(request.Token)
                || c.DocumentNumber.Contains(request.Token)
                || c.CompanyName.Contains(request.Token)
                || c.FileName.Contains(request.Token)
                || c.FolderName.Contains(request.Token)
                || c.LinkFile.Contains(request.Token)
                || c.Module.Contains(request.Token)
                || c.RelateToDocuments.Contains(request.Token)
                || c.ReplaceOf.Contains(request.Token)
                || c.ReviewNumber.Contains(request.Token)
                || c.ScopeOfApplication.Contains(request.Token)
                || c.ScopeOfDeloyment.Contains(request.Token)
                || c.Description.Contains(request.Token));
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
            List<SearchDocumentsByTokenPagedDto> data = queryResult.Select(u => u.ToSearchDocumentsByTokenPagedDto()).ToList();

            return new DataSourceResult
            {
                Data = data,
                Total = queryResult.TotalCount
            };
        }
    }
}
