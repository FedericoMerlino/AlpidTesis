using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class EventosSolidarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdItemEvento",
                table: "EventoSolidarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NombreEvento",
                table: "EventoSolidarios",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdItemEvento",
                table: "EventoSolidarios");

            migrationBuilder.DropColumn(
                name: "NombreEvento",
                table: "EventoSolidarios");
        }
    }
}
