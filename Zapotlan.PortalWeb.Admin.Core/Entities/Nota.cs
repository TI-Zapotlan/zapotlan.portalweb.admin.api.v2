using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Nota : BaseEntity
    {
        public Guid PropietarioID { get; set; }

        public string Texto { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public DateTime FechaPublicacion { get; set; }
        public PropietarioType Propietario { get; set; }
    }
}
