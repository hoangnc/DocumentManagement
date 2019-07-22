using DT.Core.Data.Models;
using MediatR;

namespace DocumentManagement.Application.PromulgateStatuses.Queries
{
    public class SearchPromulgateStatusByTokenPagedQuery : BaseSearchQuery, IRequest<DataSourceResult>
    {
    }
}
