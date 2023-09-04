using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models.Repository
{
    public class BookDbRepo : IBookRepo<Book>
    {
        UserContext db;
        public BookDbRepo(UserContext _db)
        {
            db = _db;
                
        }
       
        public void Add(Book entity)
        {
           
           db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delet(int id)
        {
            db.Books.Remove(Find(id));
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a=>a.auther).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=> a.auther).ToList();
        }

        public void Update(int id, Book newbook)
        {
            db.Books.Update(newbook);
            db.SaveChanges();
        }
        public List<Book> Search (string term)
        {
            var reslt = db.Books.Include(a => a.auther).Where(b => b.Description.Contains(term) ||
            b.auther.FullName.Contains(term) || b.Titel.Contains(term)).ToList();
            return reslt;
        }
    }
}

