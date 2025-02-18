using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.ToTable("VENDA");

            builder.HasKey(x => x.SalesId);
            builder.Property(x => x.SalesId).HasColumnName("ID_VENDA").HasColumnType("BIGINT");
            builder.Property(x => x.Date).HasColumnName("DATA").HasColumnType("DATETIME");
            builder.Property(x => x.Customer).HasColumnName("CLIENTE").HasColumnType("VARCHAR");
            builder.Property(x => x.Value).HasColumnName("VALOR_VENDA").HasColumnType("DECIMAL(18,2)");
        }
    }
}
