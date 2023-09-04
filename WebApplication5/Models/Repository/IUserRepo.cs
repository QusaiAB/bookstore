using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models.Repository
{
    public interface IUserRepo<TEntity>
    {
        IList<string> List();
        
        void Add(TEntity entity);
        void Update( TEntity entity);
        void Delet(TEntity entity);
       
    }
}
