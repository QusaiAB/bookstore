using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Models.Repository;

namespace WebApplication5.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly UserContext userContext;

        public IUserRepo<AppUser> Repo { get; }

        public UserController(UserManager <AppUser> userManager ,UserContext userContext,
            IUserRepo<AppUser> repo)
        {
            this.userManager = userManager;
            this.userContext = userContext;
            Repo = repo;
        }
        public  ActionResult Index()
        {
           var user= Repo.List();
           
            return View(user);
        }

        // GET: UserController/Details/5
        public  async  Task< ActionResult> Details(string us)
        {
          var user =  await userManager.FindByNameAsync(us);
            // var user = repo.Find(id);
            
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Create(AuthUser user)
        {
            var User1 = await userManager.FindByNameAsync(user.UserName);
            if (User1 == null)
            {
                var u = new AppUser
                {
                    UserName = user.UserName
                };
                var result = await userManager.CreateAsync(u, user.Password);
                await userManager.AddToRoleAsync(u, user.TheRole);
                return View(); }

            
            else
            { 
                ViewData.Add("erro", "User Name Is already Exist");
                return View(user);
            }
        }
    

        // GET: UserController/Edit/5
        public async Task <ActionResult> Edit(string user)
        {
            var user1 = await userManager.FindByNameAsync(user);
            return View(user1);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> cEdit(AuthUser user)
        {
            try
            {
                var User1 = await userManager.FindByNameAsync(user.UserName);
                if (User1 != null)
                {
                    var u = new AppUser
                    {
                        UserName = user.UserName
                    };
                    var result = await userManager.CreateAsync(u, user.Password);
                    await userManager.AddToRoleAsync(u, user.TheRole);
                    return View();
                }


                else
                {
                    ViewData.Add("erro", "User Name Is not found");
                    return View(user);
                }
               
            }
            catch
            {
                return View();
            }
        }

       
        }
    }
