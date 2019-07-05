using System.Linq;

namespace DT.Core.Web.Common.Models
{
    public class MenuItem : BaseMenuItem
    {
        private const string DefaultIcon = "fa fa-file-o";
        public string Name { get; set; }
        public string DisplayName { get; set; }
        private string _icon;
        public string Icon {
            get => _icon??DefaultIcon;
            set => _icon = value;
        }
        public string CssClass { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public bool IsDisabled { get; set; }
        public MenuItems Items { get; set; }
       
        public bool IsLeaf => Items == null && !Items.Any();

        private string _elementId;
        public string ElementId
        {
            get => _elementId ?? GetDefaultElementId();
            set => _elementId = value;
        }

        public MenuItem()
        {
            Items = new MenuItems();
        }

        public MenuItem AddItem(MenuItem menuItem)
        {
            Items.Add(menuItem);
            return this;
        }

        private string GetDefaultElementId()
        {
            return $"MenuItem_{Name}";
        }
    }
}
