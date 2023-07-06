using FluentValidation;
using Zapotlan.PortalWeb.Admin.Core.DTOs;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Validators
{
    public class EmpleadoPostValidators : AbstractValidator<EmpleadoAddDto>
    {
        public EmpleadoPostValidators()
        {
            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el nombre del usuario que ejecuta la acción")
                .MaximumLength(50).WithMessage("El nombre del usuario, no puede ser mayor a {Maxlength} carácteres");
        }
    }

    public class EmpleadoPutValidator : AbstractValidator<EmpleadoEditDto>
    {
        public EmpleadoPutValidator()
        {
            RuleFor(i => i.ID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del registro a actualizar");

            RuleFor(i => i.PersonaID)
                .NotEmpty().WithMessage("Faltó especificar la Persona asociada al Empleado");

            RuleFor(i => i.AreaID)
                .NotEmpty().WithMessage("Faltó especificar el área donde labora el Empleado");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el nombre del usuario que ejecuta la acción")
                .MaximumLength(50).WithMessage("El nombre del usuario, no puede ser mayor a {Maxlength} carácteres");

            // TODO: Aquí me quedé
        }
    }
}
