using DT.Core.Command;

namespace DocumentManagement.Application.Modules.Commands
{
    public class CreateModuleCommand : BaseCommand<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
