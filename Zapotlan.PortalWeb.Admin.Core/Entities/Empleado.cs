using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Empleado : BaseEntity
    {
        public Guid? AreaID { get; set; }
        public Guid? EmpleadoJefeID { get; set; }
        public Guid? PersonaID { get; set; }

        public string Codigo { get; set; } = string.Empty;
        public string NombreAreaEGob { get; set; } = string.Empty;
        public string NombrePuesto { get; set; } = string.Empty;
        public string ArchivoFotografia { get; set; } = string.Empty;
        public string ArchivoCV { get; set; } = string.Empty;
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string TipoNomina { get; set; } = string.Empty;
        public EmpleadoSincronizableType Sincronizable { get; set; }
        public EmpleadoStatusType Estatus { get; set; } = EmpleadoStatusType.Ninguno;

        // RELATIONS

        public virtual Persona? Persona { get; set; }
        public virtual Area? Area { get; set; }
        public virtual Empleado? Jefe { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
