using System;

namespace DocumentManagement.Application.PromulgateStatuses.Queries
{
    public class SearchPromulgateStatusByTokenPagedDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
