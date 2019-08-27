using MediatR;

namespace DocumentManagement.Application.Appendices.Commands
{
    public class DeleteAppendiceCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
