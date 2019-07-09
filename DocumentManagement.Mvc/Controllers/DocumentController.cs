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
    public class DocumentController : DocumentManagementControllerBase
    {
        // GET: Document
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewDocument)]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.Documents)]
        [HandleForbidden]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewDocument)]
        public ActionResult Create()
        {
            return View();
        }
    }
}