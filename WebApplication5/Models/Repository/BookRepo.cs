using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models.Repository
{
    public class BookRepo : IBookRepo<Book>
    {
        List<Book> books;
        public BookRepo()
        {
            books = new List<Book>
            {
                new Book
                {
                    Id= 5 ,Titel="c#" ,Description= "djhd " ,auther= new Auther ()
                },  new Book
                {
                    Id= 3 ,Titel="java" ,Description= "djhd " ,auther= new Auther ()
                },  new Book
                {
                    Id= 4 ,Titel="c++" ,Description= "djhd ",auther= new Auther ()
                }

            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Delet(int id)
        {
            books.Remove(Find(id));
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
           return  books.Where(a => a.Description.Contains(term)).ToList();
        }

        public void Update(int id , Book newbook)
        {
            var Book = Find(id);
            Book.Description = newbook.Description;
            Book.auther = newbook.auther;
            Book.Titel = newbook.Titel;
            Book.UrlImage = newbook.UrlImage;

        }
    }
}
