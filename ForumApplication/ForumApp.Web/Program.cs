using ForumApp.Core.Contracts;
using ForumApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using ForumApp.Infrastructure.Data;

namespace ForumApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                      ?? throw new Exception("Connection string not found");

            builder.Services.AddDbContext<ForumAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add services to the IoC container.
            builder.Services.AddControllersWithViews();

            // add application services => interface and its implementation => from ForumApp.Core layer
            builder.Services.AddScoped<IPostService, PostService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
