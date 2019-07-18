using DT.Core.Data.Models;
using MediatR;

namespace DocumentManagement.Application.Modules.Queries
{
    public class SearchModulesByTokenPagedQuery : BaseSearchQuery, IRequest<DataSourceResult>
    {
    }
}
