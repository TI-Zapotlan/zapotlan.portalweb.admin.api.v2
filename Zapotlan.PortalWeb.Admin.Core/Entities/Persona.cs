using System.Text.Json.Serialization;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Persona : BaseEntity
    {
        public string? Prefijo { get; set; }
        public string? Nombres { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? CURP { get; set; }
        public string? RFC { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaDefuncion { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PersonaEstadoVidaType EstadoVida { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstatusType Estatus { get; set; } = EstatusType.Ninguno;


    }
}
