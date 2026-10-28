using IKEA.BLL.Services.Departments;
using IKEA.DAL.Persistence.Data;
using IKEA.DAL.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Service
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>( optionbuilder => {

                optionbuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
          

            #endregion


            var app = builder.Build();

            #region Configure Kestrel Middlewares
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}
