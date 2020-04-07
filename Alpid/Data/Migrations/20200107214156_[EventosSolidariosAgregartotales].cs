using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class EventosSolidariosAgregartotales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ResultadoFinal",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalIngreso",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalida",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultadoFinal",
                table: "EventoSolidarios");

            migrationBuilder.DropColumn(
                name: "TotalIngreso",
                table: "EventoSolidarios");

            migrationBuilder.DropColumn(
                name: "TotalSalida",
                table: "EventoSolidarios");
        }
    }
}
