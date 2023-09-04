using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public AccountController(UserManager <AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            SignInManager = signInManager;
        }

        public SignInManager<AppUser> SignInManager { get; }
        
        public IActionResult Index()
        {
            return View();
        } 
       
        public  IActionResult Register()
        { return View(); }

        [HttpPost]
        public async Task<IActionResult> Register(AuthUser user)
        {
            var User1 = await userManager.FindByNameAsync(user.UserName);
            if (User1 == null)
            {
                var u = new AppUser
                {
                    UserName = user.UserName
                };
                var result = await userManager.CreateAsync(u, user.Password);
                if (result.Succeeded)
                {
                    var claims = new List<Claim>(2)
                {
                    new Claim (ClaimTypes.Name ,user.UserName),
                    new Claim (ClaimTypes.Role , user.TheRole)
                };
                    var Identity = new ClaimsIdentity(claims, "qusai");
                    var princeple = new ClaimsPrincipal(Identity);
                    await HttpContext.SignInAsync(princeple);
                    return View();
                }
                else { return View(); }

            }
            else
            {
                ViewData.Add("erro", "User Name Is already Exist");
                return View(user);
            }
        }
        [HttpGet]
        public IActionResult Login ()
           {
            return View();
             }
            [HttpPost]
        public  async Task<IActionResult> LoginAsync(AuthUser user)
            {
            var User1 = await userManager.FindByNameAsync(user.UserName);
            if (User1 != null)
            {
               var roles = await userManager.GetRolesAsync(User1);

                var claims = new List<Claim>(2)
                {
                    new Claim (ClaimTypes.Name ,user.UserName),
                    new Claim (ClaimTypes.Role , roles.ToString())
                };
                var Identity = new ClaimsIdentity(claims, "qusai");
                var princeple = new ClaimsPrincipal(Identity);
                await HttpContext.SignInAsync(princeple);
                // return RedirectToAction("Index", "BookController");
                return View();
            }
            else
            {
                ViewData.Add("erro", "User Name or password is not correct");
                return View(user);
            }
        }
    }
}
