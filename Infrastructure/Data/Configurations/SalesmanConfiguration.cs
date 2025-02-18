using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SalesmanConfiguration : IEntityTypeConfiguration<Salesman>
    {
        public void Configure(EntityTypeBuilder<Salesman> builder)
        {
            builder.ToTable("VENDEDOR");

            builder.HasKey(x => x.SalesmanId);
            builder.Property(x => x.SalesmanId).HasColumnName("ID_VENDEDOR").HasColumnType("BIGINT");
            builder.Property(x => x.Name).HasColumnName("NOME").HasColumnType("VARCHAR");
            builder.Property(x => x.Password).HasColumnName("SENHA").HasColumnType("VARCHAR");
        }
    }
}
