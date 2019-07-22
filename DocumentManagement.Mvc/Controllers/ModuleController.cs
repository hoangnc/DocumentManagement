using DocumentManagement.Application.Modules.Queries;
using DocumentManagement.Mvc.Mapper;
using DocumentManagement.Mvc.Models.Modules;
using DocumentManagement.Mvc.Services;
using DT.Core.Web.Ui.Navigation;
using System.Threading.Tasks;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers
{
    [Authorize]
    public class ModuleController : DocumentManagementControllerBase
    {
        // GET: Module
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.Modules)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.Module)]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.Modules)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.Module)]
        public ActionResult Create()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.Modules)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.Module)]
        public async Task<ActionResult> Update(int id)
        {
            GetModuleByIdDto getModuleByIdDto = await Mediator.Send(new GetModuleByIdQuery
            {
                Id = id
            });
            if (getModuleByIdDto != null)
                return View(getModuleByIdDto.ToModuleUpdateModel());
            return View(new ModuleUpdateModel());
        }
    }
}