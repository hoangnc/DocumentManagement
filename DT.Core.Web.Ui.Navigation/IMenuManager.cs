using DT.Core.Web.Common.Models;

namespace DT.Core.Web.Ui.Navigation
{
    public interface IMenuManager
    {
        string RenderToHtml();
        string SelectedMenu { get; set; }
        MenuItem FindMenu(string menuName);
        IMenuConfigurationContext MenuConfigurationContext { get; }
    }
}
