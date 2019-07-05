using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Core.Web.Common.Models
{
    public abstract class BaseMenuItem
    {
        public bool RequirePermission { get; set; }
        public string Permission { get; set; }
    }
}
