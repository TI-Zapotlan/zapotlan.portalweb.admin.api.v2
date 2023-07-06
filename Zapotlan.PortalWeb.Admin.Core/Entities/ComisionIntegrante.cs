using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class ComisionIntegrante : BaseEntity
    {
        public Guid AyuntamientoIntegranteID { get; set; }
        public Guid ComisionID { get; set; }
        public ComisionIntegranteType Tipo { get; set; } = ComisionIntegranteType.Ninguno;
        public DateTime FechaComision { get; set; }
        public EstatusType Estatus { get; set; } = EstatusType.Ninguno;

        // RELATIONS

        public virtual AyuntamientoIntegrante? AyuntamientoIntegrante { get; set; }
        public virtual Comision? Comision { get; set; }
    }
}
