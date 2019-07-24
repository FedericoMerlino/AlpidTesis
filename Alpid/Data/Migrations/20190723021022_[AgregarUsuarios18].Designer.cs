﻿// <auto-generated />
using System;
using Alpid.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alpid.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190723021022_[AgregarUsuarios18]")]
    partial class AgregarUsuarios18
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("ProductosID");

                    b.Property<int>("SociosId");

                    b.Property<decimal>("Valor");

                    b.HasKey("AlquilerID");

                    b.HasIndex("ProductosID");

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

                    b.Property<double?>("Debe");

                    b.Property<DateTime>("FechaMovimiento");

                    b.Property<double?>("Haber");

                    b.Property<string>("Observaciones")
                        .IsRequired();

                    b.Property<string>("TipoMovimiento");

                    b.Property<double?>("Total");

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

                    b.Property<double>("Importe");

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

                    b.Property<double>("Importe");

                    b.Property<string>("Observacion");

                    b.Property<int>("SociosID");

                    b.HasKey("CuotasID");

                    b.HasIndex("SociosID");

                    b.ToTable("Cuotas");
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

                    b.Property<double>("PrecioAlquiler");

                    b.Property<string>("ProductosTipo");

                    b.Property<int?>("ProveedoresID");

                    b.HasKey("PoductosID");

                    b.HasIndex("ProveedoresID");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Alpid.Models.Proveedores", b =>
                {
                    b.Property<int>("ProveedoresId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cuit")
                        .IsRequired();

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

            modelBuilder.Entity("Alpid.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contraseña");

                    b.Property<string>("NombreUsuario");

                    b.HasKey("UsuarioID");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Alpid.Models.Alquiler", b =>
                {
                    b.HasOne("Alpid.Models.Productos", "Productos")
                        .WithMany("Alquiler")
                        .HasForeignKey("ProductosID")
                        .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
