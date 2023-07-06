using System.ComponentModel.DataAnnotations.Schema;
using Zapotlan.PortalWeb.Admin.Core.Enumerations;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class Derecho : BaseEntity
    {
        [NotMapped]
        public override Guid ID { get => base.ID; set => base.ID = value; }
        public int DerechoID { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DerechoAccesoType? Acceso { get; set; }

        // RELACIONES

        public virtual ICollection<Grupo>? Grupos { get; set; }
        public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}
