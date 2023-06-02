namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class DerechoGrupo
    {
        public int DerechoID { get; set; }
        public Guid GrupoID { get; set; }

        // RELATIONS

        public virtual Derecho? Derecho { get; set; }
        public virtual Grupo? Grupo { get; set; }
    }
}
