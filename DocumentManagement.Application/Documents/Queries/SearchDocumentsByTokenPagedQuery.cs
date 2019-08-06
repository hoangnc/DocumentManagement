using DT.Core.Data.Models;
using MediatR;

namespace DocumentManagement.Application.Documents.Queries
{
    public class SearchDocumentsByTokenPagedQuery : BaseSearchQuery, IRequest<DataSourceResult>
    {
        public bool AdvancedSearch { get; set; }
    }
}
