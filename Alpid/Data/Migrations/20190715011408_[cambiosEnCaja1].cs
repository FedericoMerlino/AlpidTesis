using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnCaja1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Haber",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Debe",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Haber",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Debe",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
