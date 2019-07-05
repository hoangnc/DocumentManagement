using System.Collections.Generic;
using System.Linq;

namespace DT.Core.Web.Common.Models
{
    public class MenuItems : List<MenuItem>
    {
        public void Normalize()
        {
            RemoveEmptyItems();
            Order();
        }

        private void RemoveEmptyItems()
        {
            RemoveAll(item => item.IsLeaf && string.IsNullOrEmpty(item.Url));
        }

        private void Order()
        {
            MenuItem[] orderedItems = this.OrderBy(item => item.Order).ToArray();
            Clear();
            AddRange(orderedItems);
        }
    }
}
