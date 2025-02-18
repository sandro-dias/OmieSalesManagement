﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUTO");

            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.ProductId).HasColumnName("ID_PRODUTO").HasColumnType("VARCHAR");
            builder.Property(x => x.SalesId).HasColumnName("ID_VENDA").HasColumnType("BIGINT");
            builder.Property(x => x.Name).HasColumnName("NOME").HasColumnType("VARCHAR");
            builder.Property(x => x.Quantity).HasColumnName("QUANTIDADE").HasColumnType("INT");
            builder.Property(x => x.UnitValue).HasColumnName("VALOR_UNITARIO").HasColumnType("DECIMAL(18,2)");
            builder.Property(x => x.TotalValue).HasColumnName("VALOR_TOTAL").HasColumnType("DECIMAL(18,2)");
        }
    }
}