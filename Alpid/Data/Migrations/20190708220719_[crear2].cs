using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class crear2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_ProductoTipos_ProductoTiposPoductosTipoID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProductoTiposPoductosTipoID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProductoTiposPoductosTipoID",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "PoductosTipoID",
                table: "ProductoTipos",
                newName: "ProductosTipoID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProductosTipoID",
                table: "Productos",
                column: "ProductosTipoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_ProductoTipos_ProductosTipoID",
                table: "Productos",
                column: "ProductosTipoID",
                principalTable: "ProductoTipos",
                principalColumn: "ProductosTipoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_ProductoTipos_ProductosTipoID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProductosTipoID",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "ProductosTipoID",
                table: "ProductoTipos",
                newName: "PoductosTipoID");

            migrationBuilder.AddColumn<int>(
                name: "ProductoTiposPoductosTipoID",
                table: "Productos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProductoTiposPoductosTipoID",
                table: "Productos",
                column: "ProductoTiposPoductosTipoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_ProductoTipos_ProductoTiposPoductosTipoID",
                table: "Productos",
                column: "ProductoTiposPoductosTipoID",
                principalTable: "ProductoTipos",
                principalColumn: "PoductosTipoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
