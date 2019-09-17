using DT.Core.Data.Entities;

namespace DocumentManagement.Domain.Entities
{
    public class UserGroup : BaseEntity<int>
    {
        public string UserName { get; set; }
        public string Groups { get; set; }
    }
}
