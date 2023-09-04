using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication5
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            
            services.AddScoped<IBookRepo<Auther>, AutherDbReop>();
            services.AddScoped<IBookRepo<Book>, BookDbRepo>();
            services.AddScoped<IUserRepo<AppUser>, UsersRepo>();
            // services.AddScoped<ISignAccessRepo<User>, SigenAccessRepo>();


           

            services.AddAuthentication(x=> {
                x.DefaultScheme = "cookie";
                 
            }).AddCookie("cookie",x=> { x.Cookie.Name = "token"; });

            services.AddAuthorization(option =>
            {
                option.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser().Build();
            });

            services.AddIdentity<AppUser, IdentityRole>(option=>
            {
                option.Lockout.MaxFailedAccessAttempts = 3;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                option.Lockout.AllowedForNewUsers = true;

                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.User.RequireUniqueEmail = true;

                
            }).AddEntityFrameworkStores<UserContext>()
            .AddSignInManager<SignInManager<AppUser>>().AddUserManager<UserManager<AppUser>>();
            services.AddDbContext<UserContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("usercon"));
            });
            services.AddMvc();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

           app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

        }
    }
}
