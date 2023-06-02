using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapotlan.PortalWeb.Admin.Core.Entities
{
    public class GrupoUsuario
    {
        public Guid IdGrupo { get; set; }
        public Guid IdUsuario { get; set; }

        // RELATIONS

        public virtual Grupo? Grupo { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
