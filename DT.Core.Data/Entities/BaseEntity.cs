using System;

namespace DT.Core.Data.Entities
{
    public class BaseEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
