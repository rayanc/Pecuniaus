using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Pecuniaus.Web.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Pecuniaus.UICore.Models;
using Pecuniaus.UICore.Identity;

namespace Pecuniaus.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<User>(new UserStore()))
        {
        }

        public AccountController(UserManager<User> userManager)
        {
            UserManager = userManager;
            UserManager.PasswordValidator = new MinimumLengthValidator(5);
        }

        public UserManager<User> UserManager { get; private set; }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
         //   string hashedNewPassword = UserManager.PasswordHasher.HashPassword("admin");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //       IdentityResult result = await UserManager.AddPasswordAsync("2", "admin");

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            //if (Url.IsLocalUrl(returnUrl))
            //{
            //    return Redirect(returnUrl);
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Dashboard");
            //}
        
            return RedirectToAction("Index", "Dashboard"); // Always redirect to Dashboard page
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

    }
}