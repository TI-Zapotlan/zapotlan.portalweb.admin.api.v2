using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.QueryFilters
{
    public class AdministracionQueryFilter : DefaultQueryFilter
    {
        public string Periodo { get; set; } = string.Empty;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public EstatusType? Estatus { get; set; }

        public AdministracionOrderFilterType Orden { get; set; } = AdministracionOrderFilterType.Periodo;
    }
}
