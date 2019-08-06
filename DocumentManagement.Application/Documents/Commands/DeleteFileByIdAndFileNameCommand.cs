using MediatR;

namespace DocumentManagement.Application.Documents.Commands
{
    public class DeleteFileByIdAndFileNameCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}
