namespace DT.Core.Web.Common.Models
{
    public class ModuleMenuItem : BaseMenuItem
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Order { get; set; }
        public string Header { get; set; }
        public MenuItems Items { get; set; }
        public bool HasHeader => !string.IsNullOrEmpty(Header);

        public ModuleMenuItem()
        {
            Items = new MenuItems();
        }
        public ModuleMenuItem AddItem(MenuItem menuItem)
        {
            Items.Add(menuItem);
            return this;
        }
    }
}
