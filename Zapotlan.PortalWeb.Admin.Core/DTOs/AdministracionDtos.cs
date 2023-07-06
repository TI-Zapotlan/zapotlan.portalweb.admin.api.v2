using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.DTOs
{
    public class AdministracionItemListDto
    {
        public Guid ID { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public EstatusType Estatus { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }

        public int NoIntegrantes { get; set; } = 0;
    }

    public class AdministracionItemDetailDto {
        public Guid ID { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public EstatusType Estatus { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }

        public IEnumerable<AyuntamientoIntegrante>? Integrantes { get; set; }
    }

    public class AdministracionAddDto
    {
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class AdministracionEditDto
    {
        public Guid ID { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public EstatusType Estatus { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class AdministracionDelDto
    {
        public Guid ID { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }
}

