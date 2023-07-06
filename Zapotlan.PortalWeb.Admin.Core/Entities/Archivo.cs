using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Archivo : BaseEntity
    {
        public Guid PropietarioID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public PropietarioType Propietario { get; set; } = PropietarioType.Ninguno;
    }
}
