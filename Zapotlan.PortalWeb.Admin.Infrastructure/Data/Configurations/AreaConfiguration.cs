using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Data.Configurations
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");

            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID)
                .HasColumnName("idArea");

            builder.Property(a => a.AreaPadreID)
                .HasColumnName("idAreaPadre");

            //builder.Property(a => a.JefeAreaID)
            //    .HasColumnName("idJefeArea");

            builder.Property(a => a.Clave)
                .HasMaxLength(10);

            builder.Property(a => a.Nombre)
                .HasColumnName("NombreArea")
                .HasMaxLength(100);

            builder.Property(a => a.NombreCorto)
                .HasMaxLength(25);

            builder.Property(a => a.Descripcion)
                .HasColumnName("DescripcionArea")
                .HasMaxLength(255);

            builder.Property(a => a.UbicacionDescripcion)
                .HasMaxLength(250);

            builder.Property(a => a.Tags)
                .HasMaxLength(200);

            builder.Property(a => a.UsuarioActualizacionID)
                .HasColumnName("Usuario");

            builder.Property(a => a.UsuarioActualizacion)
                .HasMaxLength(25);

            builder.Property(a => a.FechaActualizacion)
                .HasColumnName("FechaUsuario");

            // RELATIONS

            builder.HasOne(e => e.AreaPadre)
                .WithMany(e => e.Areas)
                .HasForeignKey(e => e.AreaPadreID);

            //builder.HasMany(e => e.Empleados) // xBlaze: Este está en EmpleadosConfiguration
            //    .WithOne(e => e.Area)
            //    .HasForeignKey(e => e.AreaID);
        }
    }
}
