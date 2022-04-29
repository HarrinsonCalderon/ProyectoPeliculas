using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class Actores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
