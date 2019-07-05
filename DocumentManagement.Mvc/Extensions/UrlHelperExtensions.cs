using System.Configuration;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetMasterDataEndpoint(this UrlHelper urlHelper)
        {
            return ConfigurationManager.AppSettings["MasterDataEndpoint"].ToString();
        }
    }
}