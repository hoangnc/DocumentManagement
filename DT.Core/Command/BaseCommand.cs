using MediatR;
using System;

namespace DT.Core.Command
{
    public class BaseCommand<T> : IRequest<T>
    {
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
