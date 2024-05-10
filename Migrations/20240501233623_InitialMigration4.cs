using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetEsperancaProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ong_OngId",
                table: "Comment");

            migrationBuilder.AlterColumn<Guid>(
                name: "OngId",
                table: "Comment",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ong_OngId",
                table: "Comment",
                column: "OngId",
                principalTable: "Ong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ong_OngId",
                table: "Comment");

            migrationBuilder.AlterColumn<Guid>(
                name: "OngId",
                table: "Comment",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ong_OngId",
                table: "Comment",
                column: "OngId",
                principalTable: "Ong",
                principalColumn: "Id");
        }
    }
}
