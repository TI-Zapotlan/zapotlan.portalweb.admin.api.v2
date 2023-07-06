using System.ComponentModel.DataAnnotations.Schema;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Area : BaseEntity
    {
        public Guid? AreaPadreID { get; set; }
        //public Guid? JefeAreaID { get; set; } // Ignorar este campo
        public Guid? EmpleadoJefeID { get; set; } // Ver si se va a utilizar también
        public Guid? UbicacionID { get; set; }

        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public string? NombreCorto { get; set; }
        public string? Descripcion { get; set; }
        public AreaType? Tipo { get; set; }
        public string? UbicacionDescripcion { get; set; }
        public string? Tags { get; set; }
        public EstatusType? Estatus { get; set; }

        public Guid UsuarioActualizacionID { get; set; }

        // RELATIONS

        //public virtual Usuario? Usuario { get; set; }
        //public virtual Empleado? EmpleadoJefe { get; set; }
        public virtual Area? AreaPadre { get; set; }

        public virtual ICollection<Area>? Areas { get; set; }
        public virtual ICollection<Empleado>? Empleados { get; set; }
    }
}
