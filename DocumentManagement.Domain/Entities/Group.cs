using DT.Core.Data.Entities;

namespace DocumentManagement.Domain.Entities
{
    public class Group : BaseEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
