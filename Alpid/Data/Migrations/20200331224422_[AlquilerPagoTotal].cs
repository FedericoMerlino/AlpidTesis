using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class AlquilerPagoTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Alquiler",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorPagado",
                table: "Alquiler",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Alquiler");

            migrationBuilder.DropColumn(
                name: "ValorPagado",
                table: "Alquiler");
        }
    }
}
