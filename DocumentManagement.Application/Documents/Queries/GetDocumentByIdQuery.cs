using MediatR;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetDocumentByIdQuery : IRequest<GetDocumentByIdDto>
    {
        public int Id { get; set; }
    }
}
