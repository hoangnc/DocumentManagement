using DT.Core.Data.Models;
using MediatR;

namespace DocumentManagement.Application.Documents.Queries
{
    public class SearchDocumentsByDocumentTypeAndTokenPagedQuery : BaseSearchQuery, IRequest<DataSourceResult>
    {
        public bool AdvancedSearch { get; set; }
        public string DocumentType { get; set; }
    }
}
