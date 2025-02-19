﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BancoDeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID_PRODUTO = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    ID_VENDA = table.Column<long>(type: "BIGINT", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    QUANTIDADE = table.Column<int>(type: "INT", nullable: false),
                    VALOR_UNITARIO = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    VALOR_TOTAL = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID_PRODUTO);
                });

            migrationBuilder.CreateTable(
                name: "VENDA",
                columns: table => new
                {
                    ID_VENDA = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATA = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CLIENTE = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    VALOR_VENDA = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENDA", x => x.ID_VENDA);
                });

            migrationBuilder.CreateTable(
                name: "VENDEDOR",
                columns: table => new
                {
                    ID_VENDEDOR = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENDEDOR", x => x.ID_VENDEDOR);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUTO");

            migrationBuilder.DropTable(
                name: "VENDA");

            migrationBuilder.DropTable(
                name: "VENDEDOR");
        }
    }
}
