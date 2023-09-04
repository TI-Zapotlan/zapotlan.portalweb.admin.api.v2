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
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Personas");

            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID)
                .HasColumnName("IdPersona");

            builder.Property(e => e.Nombres)
                .HasMaxLength(100);

            builder.Property(e => e.PrimerApellido)
                .HasColumnName("ApellidoPaterno")
                .HasMaxLength(100);

            builder.Property(e => e.SegundoApellido)
                .HasColumnName("ApellidoMaterno")
                .HasMaxLength(100);

            builder.Property(e => e.CURP)                
                .HasMaxLength(18);

            builder.Property(e => e.RFC)
                .HasMaxLength(15);

            builder.Property(e => e.FechaNacimiento)
                .HasColumnType("Date");

            builder.Property(e => e.FechaDefuncion)
                .HasColumnType("Date");

            builder.Property(e => e.EstadoVida)
                .IsRequired()
                .HasColumnName("Estado");

            builder.Property(e => e.UsuarioActualizacion)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FechaActualizacion)
                .IsRequired()
                .HasColumnType("DateTime");
        }
    }
}
