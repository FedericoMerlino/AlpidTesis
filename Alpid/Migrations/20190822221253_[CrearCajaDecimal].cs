using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class CrearCajaDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cuit",
                table: "Proveedores",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Caja",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Haber",
                table: "Caja",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Debe",
                table: "Caja",
                type: "decimal(18, 2)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cuit",
                table: "Proveedores",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Haber",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Debe",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldNullable: true);
        }
    }
}
