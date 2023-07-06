namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public abstract class BaseEntity // abstract = Es para indicar que no se van a crear instancias solo heredar de ella
    {
        public virtual Guid ID { get; set; }

        public virtual string UsuarioActualizacion { get; set; } = string.Empty;

        public DateTime FechaActualizacion { get; set; }
    }
}
