using MediatR;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetDocumentByCodeQuery : IRequest<GetDocumentByCodeDto>
    {
        public string Code { get; set; }
    }
}
