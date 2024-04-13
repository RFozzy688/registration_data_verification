using Microsoft.EntityFrameworkCore;
using registration_data_verification.Data.Context;
using registration_data_verification.Data.Dal;
using registration_data_verification.Services.Hash;
using registration_data_verification.Services.Kdf;

namespace registration_data_verification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews(); // добавляем сервисы MVC
            builder.Services.AddSingleton<IHashService, ShaHashService>();
            builder.Services.AddSingleton<IKdfService, Pbkdf1Service>();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")),
                ServiceLifetime.Singleton
            );

            builder.Services.AddSingleton<DataAccessor>();

            var app = builder.Build();
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
