using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class cambiosEnAlquiler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Alquiler",
                newName: "FechaHasta");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDesde",
                table: "Alquiler",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDesde",
                table: "Alquiler");

            migrationBuilder.RenameColumn(
                name: "FechaHasta",
                table: "Alquiler",
                newName: "Fecha");
        }
    }
}
