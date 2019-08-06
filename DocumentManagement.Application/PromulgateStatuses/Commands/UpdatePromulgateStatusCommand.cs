using DT.Core.Command;

namespace DocumentManagement.Application.PromulgateStatuses.Commands
{
    public class UpdatePromulgateStatusCommand : BaseCommand<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
