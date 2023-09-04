using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        Task DeleteTmpByUser(string username);
        Task<bool> HasNamesake(Persona item); // Si tiene un homónimo
    }
}
