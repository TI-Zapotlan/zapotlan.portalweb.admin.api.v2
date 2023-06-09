﻿using System.Text.Json.Serialization;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Administracion : BaseEntity
    {
        public string? Periodo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstatusType Estatus { get; set; } = EstatusType.Ninguno;

        // RELATIONS

        public virtual ICollection<AyuntamientoIntegrante>? AyuntamientoIntegrantes { get; set; }
    }
}
