using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.DocumentTypes.Queries
{
    public class GetAllDocumentTypesQuery : IRequest<List<GetAllDocumentTypesDto>>
    {
    }
}
