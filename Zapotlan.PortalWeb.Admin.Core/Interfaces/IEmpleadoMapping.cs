using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IEmpleadoMapping
    {
        IEnumerable<EmpleadoItemListDto> EmpleadosToListDto(IEnumerable<Empleado> items);

        EmpleadoItemDetailDto EmpleadoToItemDetailDto(Empleado item);

        Empleado ItemEditDtoToEmpleado(EmpleadoEditDto itemDto);

        Empleado ItemDelDtoToEmpleado(EmpleadoDelDto itemDto);
    }
}
