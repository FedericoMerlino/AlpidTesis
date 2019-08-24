
using Alpid.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alpid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Alquiler> Alquiler { get; set; }
        public DbSet<Caja> Caja { get; set; }
        public DbSet<CuotaPrecio> CuotaPrecio { get; set; }
        public DbSet<Cuotas> Cuotas { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Socios> Socios { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<EventoSolidarios> EventoSolidarios { get; set; }
    }
}

