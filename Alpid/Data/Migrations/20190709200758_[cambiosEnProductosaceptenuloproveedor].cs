using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnProductosaceptenuloproveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_ProveedoresID",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "ProveedoresID",
                table: "Productos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedoresID",
                table: "Productos",
                column: "ProveedoresID",
                principalTable: "Proveedores",
                principalColumn: "ProveedoresId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_ProveedoresID",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "ProveedoresID",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedoresID",
                table: "Productos",
                column: "ProveedoresID",
                principalTable: "Proveedores",
                principalColumn: "ProveedoresId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
