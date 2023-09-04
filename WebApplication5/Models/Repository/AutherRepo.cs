using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models.Repository
{ 
    public class AutherRepo : IBookRepo<Auther>
    {
        List<Auther> authers ;
        public AutherRepo()
        {
            authers = new List<Auther>
            { new Auther{Id =1 , FullName = "qusai"},
             new Auther{Id =2 , FullName = "qusaiab"},

             new Auther{Id =3 , FullName = "jpud"},
            };
        }


        public void Add(Auther entity)
        {
            entity.Id = authers.Max(b => b.Id)+1;
            authers.Add(entity);
        }

        public void Delet(int id)
        {
            authers.Remove(Find(id));
        }

        public Auther Find(int id)
        {
            var auther = authers.SingleOrDefault(b => b.Id == id);
            return auther;
        }

        public IList<Auther> List()
        {
            return authers;
        }

        public List<Auther> Search(string term)
        {
          return  authers.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update( int id ,Auther entity)
        {
            var auther = Find(id);
            auther.FullName = entity.FullName;
            
        }
    }

       

       
    }
