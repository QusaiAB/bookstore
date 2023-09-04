using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models.Repository
{
    public class UsersRepo : IUserRepo<AppUser>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly UserContext userContext;

        public UsersRepo( UserManager<AppUser> userManager ,UserContext userContext)
        {
            this.userManager = userManager;
            this.userContext = userContext;
        }
        public void Add(AppUser entity)
        {
            userManager.CreateAsync(entity);
            
        }

        public void Delet(AppUser appUser)
        {
            userContext.Users.Remove(appUser);
        }

       

        public IList<string> List()
        { 
           var users=  userContext.Users.Select(x=> x.UserName).ToList();
            
            return users;
        }

      

        public void Update( AppUser entity)
        {
            userContext.Users.Update(entity);
        }

       
    }
}
