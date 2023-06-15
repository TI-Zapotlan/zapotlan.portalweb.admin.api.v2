using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.QueryFilters
{
    public class AreaQueryFilter : DefaultQueryFilter
    {
        public Guid? AreaPadreID { get; set; }
        public string Texto { get; set; } = string.Empty;
        public AreaType Tipo { get; set; } = AreaType.Ninguno;
        public EstatusType Estatus { get; set; } = EstatusType.Ninguno;

        public AreaOrderFilterType Orden {  get; set; }
    }
}
