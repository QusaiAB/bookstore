using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Models.Repository;
using WebApplication5.ViewsModels;

namespace WebApplication5.Controllers
{ [Authorize]
    public class BookController : Controller
    {
        private readonly IBookRepo<Book> repo;
        private readonly IBookRepo<Auther> repo1;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookRepo<Book> repo , IBookRepo<Auther> repo1,
            IHostingEnvironment hosting)
        {
            this.repo = repo;
            this.repo1 = repo1;
            this.hosting = hosting;
        }
         
        
        public ActionResult Index()
        {
            var books = repo.List();
            return View(books);
        }
        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var books = repo.Find(id);
            return View(books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        { var model = new AutherBookRepo { auther = FillSelect()

            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AutherBookRepo model)
        { if (ModelState.IsValid)
            {
                try
                {
                    string fillname = string.Empty;
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "UpLoads");
                        fillname = model.File.FileName;
                        string FullName = Path.Combine(uploads, fillname);
                        model.File.CopyTo(new FileStream(FullName, FileMode.Create));

                    }
                  
 
                    if (model.AutherId == -1)
                    {
                        ViewBag.Message = " please select an auther";
                        var vmodel = new AutherBookRepo
                        {
                            auther = FillSelect()

                        };
                        return View(vmodel);
                    }

                    var auther = repo1.Find(model.AutherId);

                    Book book = new Book
                    {
                        Id = model.BookId,
                        Titel = model.Titel,
                        auther = auther,
                        Description = model.Description
                        ,UrlImage= fillname
                    };
                    repo.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            var vmodel2 = new AutherBookRepo
            {
                auther = FillSelect()

            };
            ModelState.AddModelError("", " you have fill all requird ");
            return View(vmodel2);
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id )
        {
            var book = repo.Find(id);
            var autherid = book.auther.Id == null ? book.auther.Id = 0 : book.auther.Id;
            AutherBookRepo autherBook = new AutherBookRepo
            {
                auther = repo1.List().ToList(),
                Titel = book.Titel,
                Description = book.Description,
                BookId = book.Id,
                AutherId = autherid ,
                ImageUrl=book.UrlImage


            };

            return View(autherBook);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AutherBookRepo autherBook)
        {
            try
            {
                string fillname = string.Empty;
                if (autherBook.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "UpLoads");
                    fillname = autherBook.File.FileName;
                   string FullName = Path.Combine(uploads, fillname);
                    //delet old 
                    string oldfile = autherBook.ImageUrl;
                    string fulldelet = Path.Combine(uploads, oldfile);
                    if (fulldelet != FullName)
                    { //delet
                        System.IO.File.Delete(fulldelet);
                        //add new
                   autherBook.File.CopyTo(new FileStream(FullName, FileMode.Create));
                    }
                }
                var auther = repo1.Find(autherBook.AutherId);
                var book = new Book
                { Id=autherBook.BookId,
                    Description = autherBook.Description,
                    Titel = autherBook.Titel,
                    auther = auther,
                    UrlImage=fillname

                };
                repo.Update(autherBook.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = repo.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CDelete(int id)
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
        List<Auther> FillSelect ()
        {
            var auther = repo1.List().ToList();
            auther.Insert(0, new Auther { Id = -1, FullName = "-----plese select Auther---" });
            return auther;
        }
      public  ActionResult Search(string term)
        { var temp= repo.Search(term);
            return View("Index", temp);

        }
    }
}
