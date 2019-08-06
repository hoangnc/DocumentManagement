using DocumentManagement.Application.PromulgateStatuses.Queries;
using DocumentManagement.Mvc.Mapper;
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
    public class PromulgateStatusController : DocumentManagementControllerBase
    {
        // GET: PromulgateStatus
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.PromulgateStatuses)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.PromulgateStatus)]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.PromulgateStatuses)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.PromulgateStatus)]
        public ActionResult Create()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.PromulgateStatuses)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.PromulgateStatus)]
        public async Task<ActionResult> Update(int id)
        {
            GetPromulgateStatusByIdDto getPromulgateStatusByIdDto = await Mediator.Send(new GetPromulgateStatusByIdQuery
            {
                Id = id
            });

            return View(getPromulgateStatusByIdDto.ToPromulgateStatusUpdateModel());
        }
    }
}