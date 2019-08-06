using DT.Core.Data.Models;
using MediatR;

namespace DocumentManagement.Application.Appendices.Queries
{
    public class SearchAppendicesByTokenPagedQuery : BaseSearchQuery, IRequest<DataSourceResult>
    {
        public bool AdvancedSearch { get; set; }
    }
}
