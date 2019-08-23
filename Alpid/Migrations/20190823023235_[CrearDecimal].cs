using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class CrearDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioAlquiler",
                table: "Productos",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Salida",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Ingreso",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Importe",
                table: "Cuotas",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Importe",
                table: "CuotaPrecio",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Alquiler",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PrecioAlquiler",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "EventoSolidarios",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<double>(
                name: "Salida",
                table: "EventoSolidarios",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<double>(
                name: "Ingreso",
                table: "EventoSolidarios",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<double>(
                name: "Importe",
                table: "Cuotas",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<double>(
                name: "Importe",
                table: "CuotaPrecio",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Alquiler",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");
        }
    }
}
