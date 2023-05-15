using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Area : BaseEntity
    {
        public Guid? AreaPadreID { get; set; }
        public Guid? JefeAreaID { get; set; }
        public Guid? EmpleadoJefeID { get; set; }
        public Guid? UbicacionID { get; set; }

        public string Clave { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string NombreCorto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public AreaType Tipo { get; set; }
        public string UbicacionDescripcion { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public EstatusType Estatus { get; set; }

        public Guid UsuarioActualizacionID { get; set; }

        // RELACIONES

        //public Usuario Usuario { get; set; }

        //public Empleado EmpleadoJefe { get; set; }

        public virtual Area? AreaPadre { get; set; }

        public virtual ICollection<Area>? Areas { get; set; }

        public virtual ICollection<Empleado>? Empleados { get; set; }
    }
}
