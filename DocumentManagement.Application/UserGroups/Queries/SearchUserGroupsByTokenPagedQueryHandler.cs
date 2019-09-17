using DocumentManagement.Domain.Entities;
using DocumentManagement.Persistence;
using DT.Core.Data.Models;
using MediatR;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.UserGroups.Queries
{
    public class SearchUserGroupsByTokenPagedQueryHandler : IRequestHandler<SearchUserGroupsByTokenPagedQuery, DataSourceResult>
    {
        private readonly DocumentDbContext _context;
        public SearchUserGroupsByTokenPagedQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public Task<DataSourceResult> Handle(SearchUserGroupsByTokenPagedQuery request, CancellationToken cancellationToken)
        {

            List<SearchUserGroupsByTokenPagedDto> result = new List<SearchUserGroupsByTokenPagedDto>();
                result = request.Users.Where(u => {
                    string userName = u.userName;
                    return userName.Contains(request.Token) || (request.Token == null || request.Token == "");
                }).Select( u =>
                {
                    string userName = u.userName;
                    string groups = _context.UserGroups
                    .Where(ug => ug.UserName == userName)
                    .SingleOrDefault()
                    .Groups;

                    return new SearchUserGroupsByTokenPagedDto
                    {
                        UserName = userName,
                        Groups = groups
                    };
                }).ToList();                            

            return Task.FromResult(new DataSourceResult
            {
                Data = result.Skip(request.DataSourceRequest.PageNum * request.DataSourceRequest.PageSize).Take(request.DataSourceRequest.PageSize),
                Total = result.Count
            });
        }
    }
}
