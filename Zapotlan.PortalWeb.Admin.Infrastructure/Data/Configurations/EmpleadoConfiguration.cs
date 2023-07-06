using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Data.Configurations
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleados");

            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID)
                .HasColumnName("EmpleadoID");

            builder.Property(e => e.Codigo)
                .HasMaxLength(40);

            builder.Property(e => e.NombrePuesto)
                .HasMaxLength(200);

            builder.Property(e => e.ArchivoFotografia)
                .HasMaxLength(260);

            builder.Property(e => e.ArchivoCV)
                .HasMaxLength(260);

            builder.Property(e => e.FechaIngreso)
                .HasColumnType("Date");

            builder.Property(e => e.FechaTermino)
                .HasColumnType("Date");

            builder.Property(e => e.TipoNomina)
                .HasMaxLength(50);

            builder.Property(e => e.UsuarioActualizacion)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.FechaActualizacion)
                .IsRequired()
                .HasColumnType("DateTime");

            // RELATIONS

            builder.HasOne(e => e.Persona)
                .WithOne()
                .HasForeignKey<Empleado>(e => e.PersonaID);

            builder.HasOne(e => e.Area)
                .WithMany(a => a.Empleados)
                .HasForeignKey(e => e.AreaID);

            builder.HasOne(e => e.Jefe)
                .WithMany(e => e.Empleados)
                .HasForeignKey(e => e.EmpleadoJefeID);
        }
    }
}
