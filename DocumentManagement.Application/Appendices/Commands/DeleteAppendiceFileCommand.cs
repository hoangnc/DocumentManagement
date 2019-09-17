using MediatR;

namespace DocumentManagement.Application.Appendices.Commands
{
    public class DeleteAppendiceFileCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
