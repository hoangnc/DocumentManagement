using System.Web.Mvc;

namespace DT.Core.Web.Ui.Navigation
{
    public class MenuAttribute : ActionFilterAttribute
    {
        public string SelectedMenu { get; set; }
        private readonly IMenuManager _menuManager;
        public MenuAttribute()
        {
            _menuManager = DependencyResolver.Current.GetService<IMenuManager>();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _menuManager.SelectedMenu = SelectedMenu;
            filterContext.Controller.ViewBag.Menu = _menuManager;
            base.OnActionExecuting(filterContext);
        }
    }
}
