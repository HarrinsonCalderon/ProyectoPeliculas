using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class Actores2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Genero",
                table: "Genero");

            migrationBuilder.RenameTable(
                name: "Genero",
                newName: "generos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_generos",
                table: "generos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Actores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_generos",
                table: "generos");

            migrationBuilder.RenameTable(
                name: "generos",
                newName: "Genero");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genero",
                table: "Genero",
                column: "Id");
        }
    }
}
