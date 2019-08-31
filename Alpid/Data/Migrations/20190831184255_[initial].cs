﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpid.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuotaPrecio",
                columns: table => new
                {
                    CuotaPrecioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Importe = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FechaDesde = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuotaPrecio", x => x.CuotaPrecioID);
                });

            migrationBuilder.CreateTable(
                name: "EventoSolidarios",
                columns: table => new
                {
                    EventoSolidarioID = table.Column<int>(nullable: false),
                    ItemEventoSolidarioID = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Concepto = table.Column<int>(nullable: false),
                    Ingreso = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Salida = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoSolidarios", x => new { x.EventoSolidarioID, x.ItemEventoSolidarioID });
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedoresId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cuit = table.Column<string>(maxLength: 11, nullable: false),
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
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    ProviderKey = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.ProviderKey);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    PoductosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    ProductosTipo = table.Column<string>(nullable: true),
                    FechaBaja = table.Column<DateTime>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    MotivoBaja = table.Column<string>(nullable: true),
                    PrecioAlquiler = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ProveedoresID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.PoductosID);
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_ProveedoresID",
                        column: x => x.ProveedoresID,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedoresId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuotas",
                columns: table => new
                {
                    CuotasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    Importe = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FechaDesde = table.Column<DateTime>(nullable: true),
                    FechaHasta = table.Column<DateTime>(nullable: false),
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
                name: "ProductosAlquiler",
                columns: table => new
                {
                    ProductosAlquilerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<string>(nullable: true),
                    cantidad = table.Column<string>(nullable: true),
                    ProductosID = table.Column<int>(nullable: false),
                    AlquilerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosAlquiler", x => x.ProductosAlquilerID);
                    table.ForeignKey(
                        name: "FK_ProductosAlquiler_Productos_ProductosID",
                        column: x => x.ProductosID,
                        principalTable: "Productos",
                        principalColumn: "PoductosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alquiler",
                columns: table => new
                {
                    AlquilerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    FechaHasta = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    SociosId = table.Column<int>(nullable: false),
                    ProductosAlquilerID = table.Column<int>(nullable: true),
                    ProductosPoductosID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquiler", x => x.AlquilerID);
                    table.ForeignKey(
                        name: "FK_Alquiler_ProductosAlquiler_ProductosAlquilerID",
                        column: x => x.ProductosAlquilerID,
                        principalTable: "ProductosAlquiler",
                        principalColumn: "ProductosAlquilerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alquiler_Productos_ProductosPoductosID",
                        column: x => x.ProductosPoductosID,
                        principalTable: "Productos",
                        principalColumn: "PoductosID",
                        onDelete: ReferentialAction.Restrict);
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
                    Debe = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Haber = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TipoMovimiento = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: false),
                    FechaMovimiento = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    CuotaID = table.Column<int>(nullable: true),
                    CuotasID = table.Column<int>(nullable: true),
                    AlquilerID = table.Column<int>(nullable: true),
                    Usuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caja", x => x.CajaId);
                    table.ForeignKey(
                        name: "FK_Caja_Alquiler_AlquilerID",
                        column: x => x.AlquilerID,
                        principalTable: "Alquiler",
                        principalColumn: "AlquilerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Caja_Cuotas_CuotasID",
                        column: x => x.CuotasID,
                        principalTable: "Cuotas",
                        principalColumn: "CuotasID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquiler_ProductosAlquilerID",
                table: "Alquiler",
                column: "ProductosAlquilerID");

            migrationBuilder.CreateIndex(
                name: "IX_Alquiler_ProductosPoductosID",
                table: "Alquiler",
                column: "ProductosPoductosID");

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
                name: "IX_Productos_ProveedoresID",
                table: "Productos",
                column: "ProveedoresID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosAlquiler_ProductosID",
                table: "ProductosAlquiler",
                column: "ProductosID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caja");

            migrationBuilder.DropTable(
                name: "CuotaPrecio");

            migrationBuilder.DropTable(
                name: "EventoSolidarios");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Alquiler");

            migrationBuilder.DropTable(
                name: "Cuotas");

            migrationBuilder.DropTable(
                name: "ProductosAlquiler");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
