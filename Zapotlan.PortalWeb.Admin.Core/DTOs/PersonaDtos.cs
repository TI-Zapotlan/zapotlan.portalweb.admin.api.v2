using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.DTOs
{
    public class PersonaItemListDto
    {
        public Guid ID { get; set; }
        public string Prefijo { get; set; } = string.Empty;
        public string NombreCompleto { get; set;} = string.Empty;
        public string CURP { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public PersonaEstadoVidaType EstadoVida { get; set; }
        public EstatusType Estatus { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }
    }

    public class PersonaItemDetailDto
    {
        public Guid ID { get; set; }
        public string Prefijo { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string CURP { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaDefuncion { get; set; }
        public PersonaEstadoVidaType EstadoVida { get; set; }
        public EstatusType Estatus { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }
    }

    public class PersonaAddDto
    {
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class PersonaEditDto
    {
        public Guid ID { get; set; }
        public string Prefijo { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string CURP { get; set; } = string.Empty;
        public string RFC { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaDefuncion { get; set; }
        public PersonaEstadoVidaType EstadoVida { get; set; }
        public EstatusType Estatus { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class PersonaDelDto
    {
        public Guid ID { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }
}
