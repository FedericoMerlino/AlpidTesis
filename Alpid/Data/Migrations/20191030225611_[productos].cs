using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class productos : Migration
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDesde",
                table: "Cuotas",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDesde",
                table: "Cuotas",
                nullable: true,
                oldClrType: typeof(DateTime));

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
