using System.Web;
using System.Web.Mvc;

namespace DocumentManagement.Mvc.Controllers
{
    public class AccountController : Controller
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