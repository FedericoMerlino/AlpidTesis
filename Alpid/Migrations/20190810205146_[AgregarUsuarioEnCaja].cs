using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class AgregarUsuarioEnCaja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Caja",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Caja");
        }
    }
}
