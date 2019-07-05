using System.Web.Mvc;

namespace DT.Core.Web.Common.Mvc.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Menu(this HtmlHelper html, string menu)
        {
            return new MvcHtmlString(menu);
        }
    }
}
