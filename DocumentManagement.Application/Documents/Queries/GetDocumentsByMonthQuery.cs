using MediatR;
using System.Collections.Generic;

namespace DocumentManagement.Application.Documents.Queries
{
    public class GetDocumentsByMonthQuery : IRequest<List<GetDocumentsByMonthDto>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
