using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;

namespace SupermarketWEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.  
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<SupermarketContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SupermarketDB")));

            var app = builder.Build();
            builder.Services.AddAuthentication("MyCookieAuth")
               .AddCookie("MyCookieAuth", config =>
               {
                   config.Cookie.Name = "MyCookieAuth";
                   config.LoginPath = "/Account/Login";
                   config.AccessDeniedPath = "/Account/AccessDenied";
               });

            // Configure the HTTP request pipeline.  
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
