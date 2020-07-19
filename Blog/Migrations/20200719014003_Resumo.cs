using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class Resumo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resumo",
                table: "Materias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resumo",
                table: "Materias");
        }
    }
}
