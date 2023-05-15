namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public abstract class BaseEntity // abstract = Es para indicar que no se van a crear instancias solo heredar de ella
    {
        public Guid ID { get; set; }

        public string UsuarioActualizacion { get; set; } = string.Empty;

        public DateTime FechaActualizacion { get; set; }
    }
}
