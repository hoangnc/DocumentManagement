using DT.Core.Command;

namespace DocumentManagement.Application.DocumentTypes.Commands
{
    public class CreateDocumentTypeCommand : BaseCommand<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
