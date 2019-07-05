using System.Collections.Generic;
using System.Linq;

namespace DT.Core.Web.Common.Models
{
    public class ModuleMenuItems : List<ModuleMenuItem>
    {
        public void Order()
        {
            ModuleMenuItem[] orderedItems = this.OrderBy(item => item.Order).ToArray();
            Clear();
            AddRange(orderedItems);
        }
    }
}
