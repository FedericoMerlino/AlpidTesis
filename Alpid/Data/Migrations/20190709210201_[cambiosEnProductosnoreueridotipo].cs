using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnProductosnoreueridotipo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductosTipo",
                table: "Productos",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductosTipo",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
