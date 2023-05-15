using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class ComisionIntegrante : BaseEntity
    {
        public Guid AyuntamientoIntegranteID { get; set; }
        public Guid ComisionID { get; set; }
        public ComisionIntegranteType Tipo { get; set; }
        public DateTime FechaComision { get; set; }
        public EstatusType Estatus { get; set; }

        // RELACIONES

        public AyuntamientoIntegrante AyuntamientoIntegrante { get; set; } = new AyuntamientoIntegrante();
        public Comision Comision { get; set; } = new Comision();
    }
}
