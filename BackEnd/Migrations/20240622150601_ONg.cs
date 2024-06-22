using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetEsperancaProject.Migrations
{
    /// <inheritdoc />
    public partial class ONg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Ong",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sobre",
                table: "Ong",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Ong");

            migrationBuilder.DropColumn(
                name: "Sobre",
                table: "Ong");

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<double>(type: "REAL", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroDeDoacao = table.Column<int>(type: "INTEGER", nullable: false),
                    ONGid = table.Column<string>(type: "TEXT", nullable: false),
                    Objetivo = table.Column<string>(type: "TEXT", nullable: false),
                    ValorAlcancado = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorDesejado = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                });
        }
    }
}
