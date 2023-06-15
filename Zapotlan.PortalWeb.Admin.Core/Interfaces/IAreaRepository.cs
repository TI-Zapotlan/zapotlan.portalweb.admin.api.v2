using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<Area?> GetByClave(string clave);
        Task DeleteTmpByUserId(Guid id);
        Task DeleteTmpByUser(string username);
    }
}
