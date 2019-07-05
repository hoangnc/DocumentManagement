using System.Configuration;
using System.Web.Mvc;

namespace DT.Core.Web.Common.Mvc.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetHost(this UrlHelper urlHelper)
        {
            return ConfigurationManager.AppSettings["Host"].ToString();
        }

        public static string ActionWithHost(this UrlHelper urlHelper, string controller, string action = null)
        {
            string host = ConfigurationManager.AppSettings["Host"].ToString();
            return $"{host}/{controller}/{action}";
        }

        public static string ActionApiWithHost(this UrlHelper urlHelper, string controller, string action)
        {
            string host = ConfigurationManager.AppSettings["Host"].ToString();
            return $"{host}/api/{controller}/{action}";
        }
    }
}
