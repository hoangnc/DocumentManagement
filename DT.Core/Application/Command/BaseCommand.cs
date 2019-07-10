using MediatR;
using System;

namespace DT.Core.Command
{
    public class BaseCommand<T> : IRequest<T>
    {
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
