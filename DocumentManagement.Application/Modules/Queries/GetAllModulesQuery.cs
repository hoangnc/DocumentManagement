using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.Modules.Queries
{
    public class GetAllModulesQuery : IRequest<List<GetAllModulesDto>>
    {
    }
}
