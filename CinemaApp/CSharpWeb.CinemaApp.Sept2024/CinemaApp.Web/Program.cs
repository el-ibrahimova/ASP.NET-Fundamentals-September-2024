using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("SQLServer");

            // Add services to the container.
            builder.Services.AddDbContext<CinemaDbContext>(options =>
            {
                // OnConfiguration Method
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
                {

                })
                .AddEntityFrameworkStores<CinemaDbContext>()
                .AddRoles<IdentityRole<Guid>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>();


            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // this is forbidden for production, because it return what type is the error and client should not be able to see that  
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // enable HTTPS for the application
            app.UseStaticFiles(); // load files from directory wwwroot - img, css, js, ico

            app.UseRouting(); // enable default routing - Url

            app.UseAuthentication(); // First -> Who am I
            app.UseAuthorization(); // Second -> What can I do

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();  // add routing to Identity Razor pages

            app.ApplyMigrations();
            app.Run();
        }
    }
}
