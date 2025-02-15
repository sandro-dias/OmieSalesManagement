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
            base.OnModelCreating(modelBuilder);
            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
