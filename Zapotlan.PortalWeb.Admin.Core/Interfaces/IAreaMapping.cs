using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IAreaMapping
    {
        IEnumerable<AreaItemListDto> AreasToListDto(IEnumerable<Area> items);

        AreaItemDetailDto AreaToItemDetailDto(Area item);

        Area ItemEditToArea(AreaEditDto itemDto);
    }
}
