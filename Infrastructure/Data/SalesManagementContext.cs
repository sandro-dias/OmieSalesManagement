using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class SalesManagementContext : DbContext
    {
        protected SalesManagementContext() { }

        public SalesManagementContext(DbContextOptions<SalesManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("PRODUTO");

                builder.HasKey(x => x.ProductId);

                builder.Property(x => x.SalesId);
                builder.Property(x => x.ProductId);
                builder.Property(x => x.Name).HasMaxLength(100);
                builder.Property(x => x.Quantity);
                builder.Property(x => x.UnitValue).HasPrecision(18,2);
                builder.Property(x => x.TotalValue).HasPrecision(18,2);
            });

            modelBuilder.Entity<Sales>(builder =>
            {
                builder.ToTable("VENDAS");

                builder.HasKey(x => x.SalesId);
                builder.Property(x => x.SalesId).ValueGeneratedOnAdd();

                builder.Property(x => x.Customer).HasMaxLength(100);
                builder.Property(x => x.Date);
                builder.Property(x => x.Value).HasPrecision(18,2);
            });

            modelBuilder.Entity<Salesman>(builder =>
            {
                builder.ToTable("VENDEDOR");

                builder.HasKey(x => x.SalesmanId);
                builder.Property(x => x.SalesmanId).ValueGeneratedOnAdd();

                builder.Property(x => x.Name).HasMaxLength(100);
                builder.Property(x => x.Password).HasMaxLength(20);
            });


            base.OnModelCreating(modelBuilder);
            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
