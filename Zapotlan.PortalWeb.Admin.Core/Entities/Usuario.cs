using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Usuario
    {
        public Guid? AreaID { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nota { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public DateTime? FechaNaja { get; set; }
        public bool Baja { get; set; }
        public UsuarioType Tipo { get; set; } = UsuarioType.Ninguno;

        // RELATIONS

        public virtual ICollection<Grupo>? Grupos { get; set; }
        public virtual ICollection<Derecho>? Derechos { get; set; }

    }
}
