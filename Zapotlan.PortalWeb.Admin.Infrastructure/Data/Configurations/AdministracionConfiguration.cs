using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Data.Configurations
{
    public class AdministracionConfiguration : IEntityTypeConfiguration<Administracion>
    {
        public void Configure(EntityTypeBuilder<Administracion> builder)
        {
            builder.ToTable("Administraciones");

            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID)
                .HasColumnName("AdministracionID");

            builder.Property(e => e.Periodo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaInicio)
                .HasColumnType("Date");

            builder.Property(e => e.FechaTermino)
                .HasColumnType("Date");

            builder.Property(e => e.UsuarioActualizacion)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.FechaActualizacion)
                .IsRequired()
                .HasColumnType("DateTime");


        }
    }
}
