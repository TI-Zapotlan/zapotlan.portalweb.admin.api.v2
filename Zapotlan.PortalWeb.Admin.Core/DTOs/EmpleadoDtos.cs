using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.DTOs
{
    public class EmpleadoItemListDto
    {
        public Guid ID { get; set; }

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

        public string Prefijo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;        
        public string NombreArea { get; set; } = string.Empty;
    }

    public class EmpleadoItemDetailDto
    {
        public Guid ID { get; set; }
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
        public string UsuarioActualizacion { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }

        public string Prefijo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreArea { get; set; } = string.Empty;
        public string EmpleadoJefeNombre { get; set; } = string.Empty;

        public IEnumerable<EmpleadoItemListDto>? Empleados { get; set; }
    }

    public class EmpleadoAddDto
    {
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class EmpleadoEditDto
    {
        public Guid ID { get; set; }

        public Guid? AreaID { get; set; }
        public Guid? EmpleadoJefeID { get; set; }
        public Guid? PersonaID { get; set; }

        public string Codigo { get; set; } = string.Empty;
        public string NombrePuesto { get; set; } = string.Empty;
        public string ArchivoFotografia { get; set; } = string.Empty;
        public string ArchivoCV { get; set; } = string.Empty;
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string TipoNomina { get; set; } = string.Empty;
        public EmpleadoSincronizableType Sincronizable { get; set; }
        public EmpleadoStatusType Estatus { get; set; } = EmpleadoStatusType.Ninguno;
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class EmpleadoFileDto
    { 
        public Guid ID { get; set; }
        public EmpleadoFileType tipo { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    public class EmpleadoDelDto
    { 
        public Guid ID { get; set; }
        public string UsuarioActualizacion { get; set; } = string.Empty;
    }

    // Desde eGobierno

    /// <summary>
    /// Clase para obtener los elementos de empleado desde eGobierno
    /// </summary>
    public class EmpleadoEGobiernoItemDto
    {
        public Guid ID { get; set; }

        public Guid? AreaID { get; set; }
        public Guid? PersonaID { get; set; }

        public string? Codigo { get; set; }
        public string? ClaveDepartamento { get; set; }
        public string? TipoTrabajador { get; set; }
        public string? ClavePuesto { get; set; }
        public string? NombrePuesto { get; set; }
        public string? ModalidadIMSS { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? TipoNomina { get; set; }
        public DateTime FechaAlta { get; set; }
        public EmpleadoStatusType Estatus { get; set; }

        public string NombreArea { get; set; } = string.Empty;

        public string? Prefijo { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? CURP { get; set; }
        
        public string NombreCompleto { get; set; } = string.Empty;
    }

    public class MetadataDto
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class EmpleadosEGobiernoDto
    {
        public List<EmpleadoEGobiernoItemDto> Data { get; set; } = new List<EmpleadoEGobiernoItemDto>();
        public MetadataDto? Meta { get; set; }
    }
}
