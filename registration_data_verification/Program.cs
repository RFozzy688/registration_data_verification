using Microsoft.EntityFrameworkCore;
using registration_data_verification.Data.Context;
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

            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
