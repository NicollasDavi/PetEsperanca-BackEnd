using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetEsperancaProject.Migrations
{
    /// <inheritdoc />
    public partial class Comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avaliacao",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Avaliacao",
                table: "Comment",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Comment",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
