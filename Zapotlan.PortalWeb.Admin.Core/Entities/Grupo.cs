namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Grupo : BaseEntity
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        // RELATIONS

        public virtual ICollection<Usuario>? Usuarios { get; set; }
        public virtual ICollection<Derecho>? Derechos { get; set; }
    }
}
