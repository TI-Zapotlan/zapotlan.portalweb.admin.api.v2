using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Persona : BaseEntity
    {
        public string Prefijo { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public PersonaEstadoVidaType EstadoVida { get; set; }
    }
}
