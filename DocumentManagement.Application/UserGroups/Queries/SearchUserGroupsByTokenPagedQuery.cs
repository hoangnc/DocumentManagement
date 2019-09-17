using DT.Core.Data.Models;
using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.UserGroups.Queries
{
    public class SearchUserGroupsByTokenPagedQuery : BaseSearchQuery, IRequest<DataSourceResult>
    {
        public IEnumerable<dynamic> Users { get; set; }
    }
}
