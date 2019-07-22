using DT.Core.Data.Models;
using MediatR;

namespace DocumentManagement.Application.DocumentTypes.Queries
{
    public class SearchDocumentTypesByTokenPagedQuery : BaseSearchQuery, IRequest<DataSourceResult>
    {
    }
}
