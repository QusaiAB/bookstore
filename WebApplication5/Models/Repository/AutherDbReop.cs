using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models.Repository
{
    public class AutherDbReop : IBookRepo<Auther>
    {
        UserContext db;
        public AutherDbReop(UserContext _db)
        {
            db = _db;    
        }
      


        public void Add(Auther entity)
        {
           
            db.authers.Add(entity);
            db.SaveChanges();
        }

        public void Delet(int id)
        {
            db.authers.Remove(Find(id));
            db.SaveChanges();
        }

        public Auther Find(int id)
        {
            var auther = db.authers.SingleOrDefault(b => b.Id == id);
            return auther;
        }

        public IList<Auther> List()
        {
            return db.authers.ToList();
        }

        public List<Auther> Search(string term)
        {
            return db.authers.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Auther entity)
        {
            db.authers.Update(entity);
            db.SaveChanges();
        }
    }




}

