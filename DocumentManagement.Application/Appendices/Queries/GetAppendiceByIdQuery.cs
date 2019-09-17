using MediatR;

namespace DocumentManagement.Application.Appendices.Queries
{
    public class GetAppendiceByIdQuery : IRequest<GetAppendiceByIdDto>
    {
        public int Id { get; set; }
    }
}
