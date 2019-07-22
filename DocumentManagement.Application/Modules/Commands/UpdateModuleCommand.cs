using DT.Core.Command;

namespace DocumentManagement.Application.Modules.Commands
{
    public class UpdateModuleCommand : BaseCommand<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
