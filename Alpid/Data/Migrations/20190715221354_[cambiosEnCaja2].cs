using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnCaja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Caja",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Caja",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
