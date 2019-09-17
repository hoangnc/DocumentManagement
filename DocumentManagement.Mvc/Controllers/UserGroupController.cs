using DocumentManagement.Mvc.Services;
using DT.Core.Web.Ui.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    [Authorize]
    public class UserGroupController : Controller
    {
        // GET: UserGroup
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Menu(SelectedMenu = MenuNameConstants.Module)]
        public ActionResult List()
        {
            return View();
        }
    }
}