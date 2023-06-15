using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAdministracionMapping
    {
        IEnumerable<AdministracionItemListDto> AdministracionesToListDto(IEnumerable<Administracion> items);

        AdministracionItemDetailDto AdministracionToItemDetailDto(Administracion item);

        Administracion ItemEditDtoToAdministracion(AdministracionEditDto itemDto);
    }
}
