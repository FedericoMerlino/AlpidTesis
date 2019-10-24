using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class Alquiler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alquiler_Socios_SociosId",
                table: "Alquiler");

            migrationBuilder.RenameColumn(
                name: "SociosId",
                table: "Alquiler",
                newName: "SociosID");

            migrationBuilder.RenameIndex(
                name: "IX_Alquiler_SociosId",
                table: "Alquiler",
                newName: "IX_Alquiler_SociosID");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquiler_Socios_SociosID",
                table: "Alquiler",
                column: "SociosID",
                principalTable: "Socios",
                principalColumn: "SociosID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alquiler_Socios_SociosID",
                table: "Alquiler");

            migrationBuilder.RenameColumn(
                name: "SociosID",
                table: "Alquiler",
                newName: "SociosId");

            migrationBuilder.RenameIndex(
                name: "IX_Alquiler_SociosID",
                table: "Alquiler",
                newName: "IX_Alquiler_SociosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquiler_Socios_SociosId",
                table: "Alquiler",
                column: "SociosId",
                principalTable: "Socios",
                principalColumn: "SociosID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
