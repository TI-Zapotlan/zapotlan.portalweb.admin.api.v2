namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class DerechoUsuario
    {
        public int DerechoID { get; set; }
        public Guid UsuarioID { get; set; }

        // RELATIONS

        public virtual Derecho? Derecho { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
