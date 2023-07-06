using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Data
{
    public class PortalWebDbContext : DbContext
    {
        // PROPERTIES

        public DbSet<Administracion> Administraciones { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Persona> Personas { get; set; }

        // CONSTRUCTORS

        public PortalWebDbContext(DbContextOptions options) : base(options) { }

        // METHODS

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Va y busca los IEntityTypeConfiguration y los aplica
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
