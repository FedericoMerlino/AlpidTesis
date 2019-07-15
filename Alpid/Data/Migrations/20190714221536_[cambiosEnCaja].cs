using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnCaja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caja_Alquiler_AlquilerID",
                table: "Caja");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Caja");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Haber",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Debe",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "CuotaID",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AlquilerID",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Caja_Alquiler_AlquilerID",
                table: "Caja",
                column: "AlquilerID",
                principalTable: "Alquiler",
                principalColumn: "AlquilerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caja_Alquiler_AlquilerID",
                table: "Caja");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Haber",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Debe",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "CuotaID",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlquilerID",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Caja",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Caja_Alquiler_AlquilerID",
                table: "Caja",
                column: "AlquilerID",
                principalTable: "Alquiler",
                principalColumn: "AlquilerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
