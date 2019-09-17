using DocumentManagement.Application.Documents.Queries;
using DocumentManagement.Application.DocumentTypes.Queries;
using DocumentManagement.Mvc.Services;
using DT.Core.Web.Common.Identity.Extensions;
using DT.Core.Web.Ui.Navigation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    [Authorize]
    public class OperationDataController : DocumentManagementControllerBase
    {
        private readonly IMenuManager _menuManager;

        public OperationDataController(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }
        // GET: OperationData
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                _menuManager.SelectedMenu = "SDTC";
            }
            else
            {
                GetAllDocumentTypesDto documentType = SimpleCache.DocumentTypes.FirstOrDefault(dt => dt.Code.Equals(code));
                _menuManager.SelectedMenu = documentType.Code;
            }
            ViewBag.Menu = _menuManager;
            ViewBag.DocumentType = code;
            return View();
        }

        public ActionResult List2(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                _menuManager.SelectedMenu = "SDTC";
            }
            else
            {
                GetAllDocumentTypesDto documentType = SimpleCache.DocumentTypes.FirstOrDefault(dt => dt.Code.Equals(code));
                _menuManager.SelectedMenu = documentType.Code;
            }
            ViewBag.Menu = _menuManager;
            ViewBag.DocumentType = code;
            return View();
        }

        public ActionResult List3(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                _menuManager.SelectedMenu = "SDTC";
            }
            else
            {
                GetAllDocumentTypesDto documentType = SimpleCache.DocumentTypes.FirstOrDefault(dt => dt.Code.Equals(code));
                _menuManager.SelectedMenu = documentType.Code;
            }
            ViewBag.Menu = _menuManager;
            ViewBag.DocumentType = code;
            return View();
        }

        [Menu(SelectedMenu = "SDTC")]
        [HandleForbidden]
        public async Task<ActionResult> Detail(string code)
        {
            GetDocumentByCodeDto getDocumentByCodeDto = await Mediator.Send(new GetDocumentByCodeQuery
            {
                Code = code
            });

            string department = User.Identity.GetDepartment();

            if (getDocumentByCodeDto.ScopeOfDeloyment.Contains(department))
                return View(getDocumentByCodeDto);

            string userName = User.Identity.GetUserName();

            if (getDocumentByCodeDto.Auditor.Equals(userName))
                return View(getDocumentByCodeDto);

            if (getDocumentByCodeDto.Drafter.Equals(userName))
                return View(getDocumentByCodeDto);

            if (getDocumentByCodeDto.Approver.Equals(userName))
                return View(getDocumentByCodeDto);

            return RedirectToAction("List");

        }
    }
}