using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.DTOs
{
    public class AreaItemListDto
    {
        public Guid ID { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string NombreCorto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public AreaType Tipo { get; set; } = AreaType.Ninguno;
        public EstatusType Estatus { get; set; } = EstatusType.Ninguno;
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }

        public string AreaPadreNombre { get; set; } = string.Empty;
        public string EmpleadoJefeNombre { get; set; } = string.Empty;
    }

    public class AreaItemDetailDto
    {
        public Guid ID { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string NombreCorto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public AreaType Tipo { get; set; } = AreaType.Ninguno;
        public string UbicacionDescripcion { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public EstatusType Estatus { get; set; } = EstatusType.Ninguno;
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }

        public string AreaPadreNombre { get; set; } = string.Empty;
        public string EmpleadoJefeNombre { get; set; } = string.Empty;

        public IEnumerable<AreaItemListDto>? Areas { get; set; }
    }

    public class AreaAddDto
    {
        public Guid UsuarioActualizacionID { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class AreaEditDto
    {
        public Guid ID { get; set; }

        public Guid? AreaPadreID { get; set; }
        public Guid? EmpleadoJefeID { get; set; }
        public Guid? UbicacionID { get; set; }

        public string Clave { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string NombreCorto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public AreaType Tipo { get; set; } = AreaType.Ninguno;
        public string UbicacionDescripcion { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public EstatusType Estatus { get; set; } = EstatusType.Ninguno;
        public Guid UsuarioActualizacionID { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class AreaDelDto
    { 
        public Guid ID { get; set; }
        public Guid UsuarioActualizacionID { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }
}
