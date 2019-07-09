using Abp.Localization;
using Abp.Web.Configuration;
using System;
using System.Web;
using System.Web.Mvc;

namespace DT.Core.Web.Common.Mvc.Controllers
{
    public class LocalizationController : Controller
    {
        private readonly IAbpWebLocalizationConfiguration _webLocalizationConfiguration;

        public LocalizationController(IAbpWebLocalizationConfiguration webLocalizationConfiguration)
        {
            _webLocalizationConfiguration = webLocalizationConfiguration;
        }

        public virtual ActionResult ChangeCulture(string cultureName, string returnUrl = "")
        {
            if (!GlobalizationHelper.IsValidCultureCode(cultureName))
            {
                throw new Exception("Unknown language: " + cultureName + ". It must be a valid culture!");
            }

            Response.Cookies.Add(
                new HttpCookie(_webLocalizationConfiguration.CookieName, cultureName)
                {
                    Expires = DateTime.Now.AddYears(2),
                    Path = Request.ApplicationPath
                }
            );

            if (!string.IsNullOrWhiteSpace(returnUrl) && Request.Url != null)// && AbpUrlHelper.IsLocalUrl(Request.Url, returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(Request.ApplicationPath);
        }
    }
}
