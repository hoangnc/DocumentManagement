using DocumentManagement.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.Application.Modules.Queries
{
    public class GetModuleByIdQueryHandler : IRequestHandler<GetModuleByIdQuery, GetModuleByIdDto>
    {
        private readonly DocumentDbContext _context;
        public GetModuleByIdQueryHandler(DocumentDbContext context)
        {
            _context = context;
        }

        public Task<GetModuleByIdDto> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
