using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_ProductoTipos_ProductosTipoID",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "ProductoTipos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProductosTipoID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProductosTipoID",
                table: "Productos");

            migrationBuilder.AddColumn<string>(
                name: "ProductosTipo",
                table: "Productos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductosTipo",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "ProductosTipoID",
                table: "Productos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductoTipos",
                columns: table => new
                {
                    ProductosTipoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoTipos", x => x.ProductosTipoID);
                });

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
    }
}
