namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Comision : BaseEntity
    {   
        public int Indice { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Funciones { get; set; } = string.Empty;
        public string Fundamento { get; set; } = string.Empty;
        public DateTime FechaInstalacion { get; set; }

        // RELATIONS

        public virtual ICollection<ComisionIntegrante>? ComisionIntegrantes { get; set; }
    }
}
