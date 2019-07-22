using DT.Core.Command;

namespace DocumentManagement.Application.DocumentTypes.Commands
{
    public class UpdateDocumentTypeCommand : BaseCommand<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
