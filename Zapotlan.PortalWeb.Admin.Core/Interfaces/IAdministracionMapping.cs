using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAdministracionMapping
    {
        IEnumerable<AdministracionListDto> AdministracionesToListDto(IEnumerable<Administracion> items);

        AdministracionItemDto AdministracionToItemDto(Administracion item);

        Administracion ItemEditDtoToAdministracion(AdministracionEditDto itemDto);
    }
}
