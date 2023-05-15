using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class AyuntamientoIntegrante : BaseEntity
    {
        public Guid EmpleadoID { get; set; }
        public Guid AdministracionID { get; set; }

        public int Indice { get; set; }
        public AyuntamientoIntegranteType Tipo { get; set; }
        public string FraccionPolitica { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public EstatusType Estatus { get; set; }

        // RELACIONES

        public virtual Administracion Administracion { get; set; } = new Administracion();
        public virtual Empleado Empleado { get; set; }

        public virtual ICollection<ComisionIntegrante> ComisionesIntegrantes { get; set; }
    }
}
