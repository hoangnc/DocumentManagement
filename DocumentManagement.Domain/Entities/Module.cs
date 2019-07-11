using DT.Core.Data.Entities;

namespace DocumentManagement.Domain.Entities
{
    public class Module : BaseEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
