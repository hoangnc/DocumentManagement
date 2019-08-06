using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetAllDocumentQuery : IRequest<List<GetAllDocumentDto>>
    {
    }
}
