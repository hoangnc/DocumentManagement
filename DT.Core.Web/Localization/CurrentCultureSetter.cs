using Abp.Localization;
using Abp.Web.Configuration;
using Abp.Web.Localization;
using DT.Core.Text;
using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DT.Core.Web.Localization
{
    public class CurrentCultureSetter : ICurrentCultureSetter
    {
        private readonly IAbpWebLocalizationConfiguration _webLocalizationConfiguration;
        public CurrentCultureSetter()
        {
            _webLocalizationConfiguration = DependencyResolver.Current.GetService<IAbpWebLocalizationConfiguration>();
        }

        public virtual void SetCurrentCulture(HttpContext httpContext)
        {
            if (IsCultureSpecifiedInGlobalizationConfig())
            {
                return;
            }

            // 1: Query String
            string culture = GetCultureFromQueryString(httpContext);
            if (culture != null)
            {
                SetCurrentCulture(culture);
                return;
            }

            // 2 & 3: Header / Cookie
            culture = GetCultureFromHeader(httpContext) ?? GetCultureFromCookie(httpContext);
            if (culture != null)
            {
                SetCurrentCulture(culture);
                return;
            }

            // 4 & 5: Default / Browser
            culture = GetBrowserCulture(httpContext);
            if (culture != null)
            {
                SetCurrentCulture(culture);
                SetCultureToCookie(httpContext, culture);
            }
        }

        protected virtual bool IsCultureSpecifiedInGlobalizationConfig()
        {
            GlobalizationSection globalizationSection = WebConfigurationManager.GetSection("system.web/globalization") as GlobalizationSection;
            if (globalizationSection == null || globalizationSection.UICulture.IsNullOrEmpty())
            {
                return false;
            }

            return !string.Equals(globalizationSection.UICulture, "auto", StringComparison.InvariantCultureIgnoreCase);
        }

        protected virtual string GetCultureFromCookie(HttpContext httpContext)
        {
            string culture = httpContext.Request.Cookies[_webLocalizationConfiguration.CookieName]?.Value;
            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual void SetCultureToCookie(HttpContext context, string culture)
        {
            context.Response.SetCookie(
                new HttpCookie(_webLocalizationConfiguration.CookieName, culture)
                {
                    Expires = DateTime.Now.AddYears(2),
                    Path = context.Request.ApplicationPath
                }
            );
        }

        protected virtual string GetCultureFromHeader(HttpContext httpContext)
        {
            string culture = httpContext.Request.Headers[_webLocalizationConfiguration.CookieName];
            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual string GetBrowserCulture(HttpContext httpContext)
        {
            if (httpContext.Request.UserLanguages.Any())
            {
                return null;
            }

            return httpContext.Request?.UserLanguages?.FirstOrDefault(GlobalizationHelper.IsValidCultureCode);
        }

        protected virtual string GetCultureFromQueryString(HttpContext httpContext)
        {
            string culture = httpContext.Request.QueryString[_webLocalizationConfiguration.CookieName];
            if (culture.IsNullOrEmpty() || !GlobalizationHelper.IsValidCultureCode(culture))
            {
                return null;
            }

            return culture;
        }

        protected virtual void SetCurrentCulture(string language)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Thread.CurrentThread.CurrentCulture = CultureInfoHelper.Get(language);
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            Thread.CurrentThread.CurrentUICulture = CultureInfoHelper.Get(language);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
