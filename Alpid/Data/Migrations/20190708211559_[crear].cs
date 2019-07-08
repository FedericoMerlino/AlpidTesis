using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Data.Migrations
{
    public partial class crear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuotaPrecio",
                columns: table => new
                {
                    CuotaPrecioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Importe = table.Column<decimal>(nullable: false),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    FechaHasta = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuotaPrecio", x => x.CuotaPrecioID);
                });

            migrationBuilder.CreateTable(
                name: "ProductoTipos",
                columns: table => new
                {
                    PoductosTipoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoTipos", x => x.PoductosTipoID);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedoresId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cuit = table.Column<string>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: false),
                    Domicilio = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: false),
                    Mail = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    MotivoBaja = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedoresId);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    SociosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cuit = table.Column<string>(maxLength: 11, nullable: false),
                    RazonSocial = table.Column<string>(nullable: false),
                    Domicilio = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    MotivoBaja = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.SociosID);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    PoductosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    FechaCompra = table.Column<DateTime>(nullable: false),
                    PrecioCompra = table.Column<decimal>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    MotivoBaja = table.Column<string>(nullable: true),
                    PrecioAlquiler = table.Column<decimal>(nullable: false),
                    ProductosTipoID = table.Column<int>(nullable: false),
                    ProductoTiposPoductosTipoID = table.Column<int>(nullable: true),
                    ProveedoresID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.PoductosID);
                    table.ForeignKey(
                        name: "FK_Productos_ProductoTipos_ProductoTiposPoductosTipoID",
                        column: x => x.ProductoTiposPoductosTipoID,
                        principalTable: "ProductoTipos",
                        principalColumn: "PoductosTipoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_ProveedoresID",
                        column: x => x.ProveedoresID,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedoresId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuotas",
                columns: table => new
                {
                    CuotasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<bool>(nullable: false),
                    Observacion = table.Column<decimal>(nullable: false),
                    FechaPago = table.Column<DateTime>(nullable: true),
                    FechaEmicion = table.Column<DateTime>(nullable: false),
                    SociosID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuotas", x => x.CuotasID);
                    table.ForeignKey(
                        name: "FK_Cuotas_Socios_SociosID",
                        column: x => x.SociosID,
                        principalTable: "Socios",
                        principalColumn: "SociosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alquiler",
                columns: table => new
                {
                    AlquilerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    ProductosID = table.Column<int>(nullable: false),
                    SociosId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquiler", x => x.AlquilerID);
                    table.ForeignKey(
                        name: "FK_Alquiler_Productos_ProductosID",
                        column: x => x.ProductosID,
                        principalTable: "Productos",
                        principalColumn: "PoductosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alquiler_Socios_SociosId",
                        column: x => x.SociosId,
                        principalTable: "Socios",
                        principalColumn: "SociosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Caja",
                columns: table => new
                {
                    CajaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Debe = table.Column<decimal>(nullable: false),
                    Haber = table.Column<decimal>(nullable: false),
                    TipoMovimiento = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    FechaMovimiento = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    CuotaID = table.Column<int>(nullable: false),
                    CuotasID = table.Column<int>(nullable: true),
                    AlquilerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.CajaId);
                    table.ForeignKey(
                        name: "FK_Caja_Alquiler_AlquilerID",
                        column: x => x.AlquilerID,
                        principalTable: "Alquiler",
                        principalColumn: "AlquilerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caja_Cuotas_CuotasID",
                        column: x => x.CuotasID,
                        principalTable: "Cuotas",
                        principalColumn: "CuotasID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquiler_ProductosID",
                table: "Alquiler",
                column: "ProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_Alquiler_SociosId",
                table: "Alquiler",
                column: "SociosId");

            migrationBuilder.CreateIndex(
                name: "IX_Caja_AlquilerID",
                table: "Caja",
                column: "AlquilerID");

            migrationBuilder.CreateIndex(
                name: "IX_Caja_CuotasID",
                table: "Caja",
                column: "CuotasID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuotas_SociosID",
                table: "Cuotas",
                column: "SociosID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProductoTiposPoductosTipoID",
                table: "Productos",
                column: "ProductoTiposPoductosTipoID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedoresID",
                table: "Productos",
                column: "ProveedoresID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caja");

            migrationBuilder.DropTable(
                name: "CuotaPrecio");

            migrationBuilder.DropTable(
                name: "Alquiler");

            migrationBuilder.DropTable(
                name: "Cuotas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "ProductoTipos");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
