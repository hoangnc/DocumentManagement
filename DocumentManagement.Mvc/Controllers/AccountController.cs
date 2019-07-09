using System.Web;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    public class AccountController : DocumentManagementControllerBase
    {
        [Route("account/signout")]
        [AllowAnonymous]
        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

    }
}