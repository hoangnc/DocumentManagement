using MediatR;

namespace DocumentManagement.Application.DocumentTypes.Queries
{
    public class GetDocumentTypeByIdQuery : IRequest<GetDocumentTypeByIdDto>
    {
        public int Id { get; set; }
    }
}
