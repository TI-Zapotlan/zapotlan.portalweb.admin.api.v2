using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.QueryFilters
{
    public class EmpleadoQueryFilter : DefaultQueryFilter
    {
        public Guid? AreaID { get; set; }
        public Guid? EmpleadoJefeID { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string TipoNomina { get; set; } = string.Empty;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public EmpleadoFechaType? FechaTipo { get; set; }

        public EmpleadoStatusType? Estatus { get; set; }

        public EmpleadoOrderFilterType Orden { get; set; } = EmpleadoOrderFilterType.Codigo;
    }
}
