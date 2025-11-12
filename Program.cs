using Microsoft.EntityFrameworkCore;
using SupermercadoCRUD.Data;
using SupermercadoCRUD2.Data.Repositorios;
using SupermercadoCRUD2.Servicios;

namespace SupermercadoCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configuración de la cadena de conexión
            string cadena = "Server=localhost;Database=ventas;Uid=root;Pwd=curso;";
            
            builder.Services.AddDbContext<SupermercadoContext>(options =>
            {
                options.UseMySql(cadena, ServerVersion.AutoDetect(cadena));
                // Habilitar logs detallados solo en desarrollo
                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });
            
            // Repositorios
            builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();
            
            // Servicios
            builder.Services.AddScoped<ServicioVentas>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                // En desarrollo, mostrar página de excepciones detallada
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Ventas}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
