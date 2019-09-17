using DocumentManagement.Application.Appendices.Queries;
using DocumentManagement.Mvc.Services;
using DT.Core.Web.Ui.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewAppendice)]
        public async Task<ActionResult> Update(int id)
        {
            GetAppendiceByIdDto getAppendiceByIdDto = new GetAppendiceByIdDto();

            getAppendiceByIdDto = await Mediator.Send(new GetAppendiceByIdQuery
            {
                Id = id
            });

            return View(getAppendiceByIdDto);
        }
    }
}