using Microsoft.EntityFrameworkCore;
using SupermercadoCRUD2.Models.Entity;

namespace SupermercadoCRUD.Data
{
    public class SupermercadoContext : DbContext
    {
        public SupermercadoContext(DbContextOptions<SupermercadoContext> options)
            : base(options)
        {
        }

        public DbSet<Venta> Ventas { get; set; }

        // Solo incluir Productos y Categorias si existen en la base de datos
        // public DbSet<Producto> Productos { get; set; }
        // public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla venta
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("venta");
                entity.HasKey(e => e.IdVenta);
                entity.Property(e => e.IdVenta).HasColumnName("id_venta");
                entity.Property(e => e.Producto).HasColumnName("producto").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Cantidad).HasColumnName("cantidad").IsRequired();
                entity.Property(e => e.Precio).HasColumnName("precio").HasColumnType("decimal(6,2)").IsRequired();
            });
        }
    }
}
