using DocumentManagement.Mvc.Services;
using DT.Core.Web.Ui.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    public class OperationDataController : Controller
    {
        // GET: OperationData
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string code)
        {
            var documentType = SimpleCache.DocumentTypes.FirstOrDefault(dt => dt.Code.Equals(code));
             IMenuManager menuManager = DependencyResolver.Current.GetService<IMenuManager>();
            menuManager.SelectedMenu = documentType.Code;
            ViewBag.Menu = menuManager;
            ViewBag.DocumentType = code;
            return View();
        }
    }
}