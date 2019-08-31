﻿// <auto-generated />
using System;
using Alpid.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alpid.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alpid.Models.Alquiler", b =>
                {
                    b.Property<int>("AlquilerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaDesde");

                    b.Property<DateTime>("FechaHasta");

                    b.Property<string>("Observacion");

                    b.Property<int?>("ProductosAlquilerID");

                    b.Property<int?>("ProductosPoductosID");

                    b.Property<int>("SociosId");

                    b.HasKey("AlquilerID");

                    b.HasIndex("ProductosAlquilerID");

                    b.HasIndex("ProductosPoductosID");

                    b.HasIndex("SociosId");

                    b.ToTable("Alquiler");
                });

            modelBuilder.Entity("Alpid.Models.Caja", b =>
                {
                    b.Property<int>("CajaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlquilerID");

                    b.Property<int?>("CuotaID");

                    b.Property<int?>("CuotasID");

                    b.Property<decimal?>("Debe")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("FechaMovimiento");

                    b.Property<decimal?>("Haber")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Observaciones")
                        .IsRequired();

                    b.Property<string>("TipoMovimiento");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Usuario");

                    b.HasKey("CajaId");

                    b.HasIndex("AlquilerID");

                    b.HasIndex("CuotasID");

                    b.ToTable("Caja");
                });

            modelBuilder.Entity("Alpid.Models.CuotaPrecio", b =>
                {
                    b.Property<int>("CuotaPrecioID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaDesde");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("CuotaPrecioID");

                    b.ToTable("CuotaPrecio");
                });

            modelBuilder.Entity("Alpid.Models.Cuotas", b =>
                {
                    b.Property<int>("CuotasID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Estado");

                    b.Property<DateTime?>("FechaDesde");

                    b.Property<DateTime>("FechaHasta");

                    b.Property<decimal>("Importe")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Observacion");

                    b.Property<int>("SociosID");

                    b.HasKey("CuotasID");

                    b.HasIndex("SociosID");

                    b.ToTable("Cuotas");
                });

            modelBuilder.Entity("Alpid.Models.EventoSolidarios", b =>
                {
                    b.Property<int>("EventoSolidarioID");

                    b.Property<int>("ItemEventoSolidarioID");

                    b.Property<int?>("Cantidad");

                    b.Property<string>("Concepto")
                        .IsRequired();

                    b.Property<DateTime>("Fecha");

                    b.Property<decimal?>("Ingreso")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("Salida")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("EventoSolidarioID", "ItemEventoSolidarioID");

                    b.ToTable("EventoSolidarios");
                });

            modelBuilder.Entity("Alpid.Models.Productos", b =>
                {
                    b.Property<int>("PoductosID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime?>("FechaBaja");

                    b.Property<string>("MotivoBaja");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<decimal>("PrecioAlquiler")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ProductosTipo");

                    b.Property<int?>("ProveedoresID");

                    b.HasKey("PoductosID");

                    b.HasIndex("ProveedoresID");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Alpid.Models.ProductosAlquiler", b =>
                {
                    b.Property<int>("ProductosAlquilerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlquilerID");

                    b.Property<int>("ProductosID");

                    b.Property<string>("Valor");

                    b.Property<string>("cantidad");

                    b.HasKey("ProductosAlquilerID");

                    b.HasIndex("ProductosID");

                    b.ToTable("ProductosAlquiler");
                });

            modelBuilder.Entity("Alpid.Models.Proveedores", b =>
                {
                    b.Property<int>("ProveedoresId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cuit")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Domicilio");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime?>("FechaBaja");

                    b.Property<string>("Mail");

                    b.Property<string>("MotivoBaja");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("Telefono")
                        .IsRequired();

                    b.HasKey("ProveedoresId");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Alpid.Models.Socios", b =>
                {
                    b.Property<int>("SociosID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cuit")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Domicilio");

                    b.Property<string>("Email");

                    b.Property<DateTime>("FechaAlta");

                    b.Property<DateTime?>("FechaBaja");

                    b.Property<string>("MotivoBaja");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("Telefono")
                        .IsRequired();

                    b.HasKey("SociosID");

                    b.ToTable("Socios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("ProviderKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId");

                    b.HasKey("ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleId");

                    b.HasKey("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Alpid.Models.Alquiler", b =>
                {
                    b.HasOne("Alpid.Models.ProductosAlquiler")
                        .WithMany("Alquiler")
                        .HasForeignKey("ProductosAlquilerID");

                    b.HasOne("Alpid.Models.Productos")
                        .WithMany("Alquiler")
                        .HasForeignKey("ProductosPoductosID");

                    b.HasOne("Alpid.Models.Socios", "Socios")
                        .WithMany("Alquiler")
                        .HasForeignKey("SociosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alpid.Models.Caja", b =>
                {
                    b.HasOne("Alpid.Models.Alquiler", "Alquiler")
                        .WithMany("Caja")
                        .HasForeignKey("AlquilerID");

                    b.HasOne("Alpid.Models.Cuotas", "Cuotas")
                        .WithMany("Caja")
                        .HasForeignKey("CuotasID");
                });

            modelBuilder.Entity("Alpid.Models.Cuotas", b =>
                {
                    b.HasOne("Alpid.Models.Socios", "Socios")
                        .WithMany("Cuotas")
                        .HasForeignKey("SociosID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alpid.Models.Productos", b =>
                {
                    b.HasOne("Alpid.Models.Proveedores", "Proveedores")
                        .WithMany("Productos")
                        .HasForeignKey("ProveedoresID");
                });

            modelBuilder.Entity("Alpid.Models.ProductosAlquiler", b =>
                {
                    b.HasOne("Alpid.Models.Productos", "Productos")
                        .WithMany()
                        .HasForeignKey("ProductosID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
