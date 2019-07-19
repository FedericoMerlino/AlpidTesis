using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnCuotasImporte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaPago",
                table: "Cuotas",
                newName: "FechaDesde");

            migrationBuilder.RenameColumn(
                name: "FechaEmicion",
                table: "Cuotas",
                newName: "FechaHasta");

            migrationBuilder.AlterColumn<string>(
                name: "Observacion",
                table: "Cuotas",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<double>(
                name: "Importe",
                table: "Cuotas",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Importe",
                table: "Cuotas");

            migrationBuilder.RenameColumn(
                name: "FechaHasta",
                table: "Cuotas",
                newName: "FechaEmicion");

            migrationBuilder.RenameColumn(
                name: "FechaDesde",
                table: "Cuotas",
                newName: "FechaPago");

            migrationBuilder.AlterColumn<decimal>(
                name: "Observacion",
                table: "Cuotas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
