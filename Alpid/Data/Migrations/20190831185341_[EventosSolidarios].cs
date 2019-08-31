using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class EventosSolidarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salida",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Ingreso",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<string>(
                name: "Concepto",
                table: "EventoSolidarios",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "EventoSolidarios",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salida",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ingreso",
                table: "EventoSolidarios",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Concepto",
                table: "EventoSolidarios",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "EventoSolidarios",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
