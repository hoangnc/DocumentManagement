using MediatR;

namespace DocumentManagement.Application.Modules.Queries
{
    public class GetModuleByIdQuery : IRequest<GetModuleByIdDto>
    {
        public int Id { get; set; }
    }
}
