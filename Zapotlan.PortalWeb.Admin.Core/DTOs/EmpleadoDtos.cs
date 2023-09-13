using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.DTOs
{
    public class EmpleadoItemListDto
    {
        public Guid ID { get; set; }

        public string Codigo { get; set; } = string.Empty;
        public string NombrePuesto { get; set; } = string.Empty;
        public string ArchivoFotografia { get; set; } = string.Empty;
        public string ArchivoCV { get; set; } = string.Empty;
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string TipoNomina { get; set; } = string.Empty;
        public EmpleadoStatusType Estatus { get; set; } = EmpleadoStatusType.Ninguno;

        public string Prefijo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;        
        public string NombreArea { get; set; } = string.Empty;
    }

    public class EmpleadoItemDetailDto
    {
        public Guid ID { get; set; }

        public string Codigo { get; set; } = string.Empty;
        public string NombrePuesto { get; set; } = string.Empty;
        public string ArchivoFotografia { get; set; } = string.Empty;
        public string ArchivoCV { get; set; } = string.Empty;
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string TipoNomina { get; set; } = string.Empty;
        public EmpleadoStatusType Estatus { get; set; } = EmpleadoStatusType.Ninguno;
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }

        public string Prefijo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreArea { get; set; } = string.Empty;
        public string EmpleadoJefeNombre { get; set; } = string.Empty;

        public IEnumerable<EmpleadoItemListDto>? Empleados { get; set; }
    }

    public class EmpleadoAddDto
    {
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class EmpleadoEditDto
    {
        public Guid ID { get; set; }

        public Guid? AreaID { get; set; }
        public Guid? EmpleadoJefeID { get; set; }
        public Guid? PersonaID { get; set; }

        public string Codigo { get; set; } = string.Empty;
        public string NombrePuesto { get; set; } = string.Empty;
        public string ArchivoFotografia { get; set; } = string.Empty;
        public string ArchivoCV { get; set; } = string.Empty;
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string TipoNomina { get; set; } = string.Empty;
        public EmpleadoStatusType Estatus { get; set; } = EmpleadoStatusType.Ninguno;
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class EmpleadoDelDto
    { 
        public Guid ID { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }
}
