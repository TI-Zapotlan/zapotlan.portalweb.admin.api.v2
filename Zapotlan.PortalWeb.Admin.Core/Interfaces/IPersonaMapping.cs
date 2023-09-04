using Zapotlan.PortalWeb.Admin.Core.DTOs;
using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IPersonaMapping
    {
        IEnumerable<PersonaItemListDto> PersonasToListDto(IEnumerable<Persona> items);

        PersonaItemDetailDto PersonaToItemDetailDto(Persona item);

        Persona ItemEditDtoToPersona(PersonaEditDto itemDto);

        Persona ItemDelDtoToPersona(PersonaDelDto itemDto);
    }
}
