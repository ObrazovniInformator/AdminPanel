using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> SignInManager;
        public AdministrationController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> userManager)
        {
            this.SignInManager = SignInManager;
            this.userManager = userManager;
        }
       
       public IActionResult Index()
        {
            if (SignInManager.IsSignedIn(User))
            {
                var user = userManager.Users.ToList();
                ViewBag.Lista = user;
                return View();

            }
            else
            {
                return RedirectPermanent("~/Areas/Identity/Login");
            }
            
        }
    }
}