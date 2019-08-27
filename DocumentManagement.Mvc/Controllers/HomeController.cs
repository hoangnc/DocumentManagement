using DocumentManagement.Mvc.Services;
using DT.Core.Web.Ui.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewAppendice)]
        public ActionResult Index()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewAppendice)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}