using Abp.Web.Localization;
using System.Web;
using System.Web.Mvc;

namespace DocumentManagement.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_BeginRequest()
        {
            DependencyResolver.Current.GetService<ICurrentCultureSetter>().SetCurrentCulture(Context);
        }

        protected void Application_Start()
        {

        }
    }
}
