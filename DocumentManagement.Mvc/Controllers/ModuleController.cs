using DocumentManagement.Mvc.Services;
using DT.Core.Web.Ui.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers
{
    [Authorize]
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.Module)]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.Module)]
        public ActionResult Create()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.Module)]
        public ActionResult Update()
        {
            return View();
        }
    }
}