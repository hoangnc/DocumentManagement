using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Mvc.Mapper;
using DocumentManagement.Mvc.Services;
using DT.Core.Web.Ui.Navigation;
using System.Threading.Tasks;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers
{
    public class DocumentTypeController : DocumentManagementControllerBase
    {
        // GET: DocumentType
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.DocumentTypes)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.DocumentType)]
        public ActionResult List()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.DocumentTypes)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.DocumentType)]
        public ActionResult Create()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.DocumentTypes)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.DocumentType)]
        public async Task<ActionResult> Update(int id)
        {
            GetDocumentTypeByIdDto getDocumentTypeByIdDto = await Mediator.Send(new GetDocumentTypeByIdQuery {
                Id = id
            });

            return View(getDocumentTypeByIdDto.ToDocumentTypeUpdateModel());
        }
    }
}