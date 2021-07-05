using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
       // private readonly RoleManager<IdentityRole> roleManager;
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