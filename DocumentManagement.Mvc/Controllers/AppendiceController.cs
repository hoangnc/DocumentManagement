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
    public class AppendiceController : DocumentManagementControllerBase
    {
        // GET: Appendice
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewAppendice)]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.Appendices)]
        [HandleForbidden]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.Appendices)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewAppendice)]
        public ActionResult Create()
        {
            return View();
        }
    }
}