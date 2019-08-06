using MediatR;

namespace DocumentManagement.Application.PromulgateStatuses.Queries
{
    public class GetPromulgateStatusByIdQuery : IRequest<GetPromulgateStatusByIdDto>
    {
        public int Id { get; set; }
    }
}
