using Microsoft.EntityFrameworkCore;
using Proyecto_Laboratotio_Back2.Entities;

namespace Proyecto_Laboratotio_Back2.Data
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) //constructor para inyeccion de dbcontext
        {

        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; } //creacion de las tablas de la db
        public DbSet<ProductsSales> ProductsSales { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductsSales>()
            .HasKey(ps => new { ps.SaleId, ps.ProductId }); // Definir la clave primaria compuesta

            builder.Entity<ProductsSales>()
                .HasOne(ps => ps.Sale)
                .WithMany(s => s.ProductsSales)
                .HasForeignKey(ps => ps.SaleId);

            builder.Entity<ProductsSales>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductsSales)
                .HasForeignKey(ps => ps.ProductId);
        }
    }
}
