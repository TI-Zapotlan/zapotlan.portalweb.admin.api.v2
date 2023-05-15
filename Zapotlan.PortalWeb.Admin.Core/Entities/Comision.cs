namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Comision : BaseEntity
    {   
        public int Indice { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Funciones { get; set; }
        public string? Fundamento { get; set; }
        public DateTime FechaInstalacion { get; set; }

        // RELACIONES

        public virtual ICollection<ComisionIntegrante>? ComisionIntegrantes { get; set; }
    }
}
