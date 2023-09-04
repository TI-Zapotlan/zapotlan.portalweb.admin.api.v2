using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.QueryFilters
{
    public class PersonaQueryFilter : DefaultQueryFilter
    {
        public string? Texto { get; set; }
        public PersonaEstadoVidaType EstadoVida { get; set; } = PersonaEstadoVidaType.Ninguno;
        public EstatusType? Estatus { get; set; }
        public PersonaOrderFilterType Orden { get; set; } = PersonaOrderFilterType.Ninguno;
    }
}
