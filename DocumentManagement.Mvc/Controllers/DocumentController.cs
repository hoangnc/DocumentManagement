using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Mvc.Models.Documents;
using DocumentManagement.Mvc.Services;
using DT.Core.Authorization;
using DT.Core.Web.Ui.Navigation;
using System.Threading.Tasks;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;
using static DT.Core.Web.Common.Identity.Constants;

namespace DocumentManagement.Mvc.Controllers
{
    [Authorize]
    public class DocumentController : DocumentManagementControllerBase
    {
        private readonly IPermissionChecker _permissionChecker;

        public DocumentController(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        // GET: Document
        public ActionResult Index()
        {
            if (_permissionChecker.IsGranted("admindocumentmvc_permission", "write"))
                return RedirectToAction("List");
            return RedirectToAction("List", "OperationData");
        }

        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewDocument)]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.Documents)]
        [HandleForbidden]
        public ActionResult List()
        {
            return View();
        }

        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewDocument)]
        [ResourceAuthorize(DtPermissionBaseTypes.Read, DocumentResources.Documents)]
        [HandleForbidden]
        public async Task<ActionResult> Detail(string code)
        {
            GetDocumentByCodeDto getDocumentByCodeDto = await Mediator.Send(new GetDocumentByCodeQuery
            {
                Code = code
            });

            return View(getDocumentByCodeDto);
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewDocument)]
        public ActionResult Create()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Write, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.ReviewDocument)]
        public ActionResult ReviewDocument()
        {
            return View();
        }

        [ResourceAuthorize(DtPermissionBaseTypes.Update, DocumentResources.Documents)]
        [HandleForbidden]
        [Menu(SelectedMenu = MenuNameConstants.ReleaseNewDocument)]
        public async Task<ActionResult> Update(int id)
        {
            DocumentUpdateModel documentUpdateModel = new DocumentUpdateModel();

            GetDocumentByIdDto getDocumentByIdDto = await Mediator.Send(new GetDocumentByIdQuery
            {
                Id = id
            });

            return View(getDocumentByIdDto);
        }
    }
}