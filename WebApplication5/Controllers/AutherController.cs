using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Models.Repository;

namespace WebApplication5.Controllers
{ [Authorize]
    public class AutherController : Controller
    {
        private readonly IBookRepo<Auther> repo;

        public AutherController(IBookRepo<Auther> repo)
        {
            this.repo = repo;
        }
        // GET: AutherController
        public ActionResult Index()
        {
            var auther = repo.List();
            return View(auther);
        }

        // GET: AutherController/Details/5
        public ActionResult Details(int id)
        {
            var auther = repo.Find(id);
            return View(auther);
        }

        // GET: AutherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auther auther)
        {
            try
            {
                repo.Add(auther);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Edit/5
        public ActionResult Edit(int id)
        {
            var auther = repo.Find(id);
            return View(auther);
        }

        // POST: AutherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Auther auther)
        {
            try
            {
                repo.Update(id ,auther);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutherController/Delete/5
        public ActionResult Delete(int id)
        {
            var auther = repo.Find(id);
            return View(auther);
        }

        // POST: AutherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Auther auther)
        {
            try
            {
                repo.Delet(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
