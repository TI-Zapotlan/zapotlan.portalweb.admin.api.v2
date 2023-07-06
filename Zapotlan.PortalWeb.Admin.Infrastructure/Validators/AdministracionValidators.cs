using FluentValidation;
using Zapotlan.PortalWeb.Admin.Core.DTOs;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Validators
{
    public class AdministracionPostValidator : AbstractValidator<AdministracionAddDto>
    {
        public AdministracionPostValidator()
        {
            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el nombre del usuario que ejecuta la acción")
                .MaximumLength(25).WithMessage("El nombre del usuario, no puede ser mayor a {Maxlength} carácteres");
        }
    }

    public class AdministracionPutValidator : AbstractValidator<AdministracionEditDto>
    {
        public AdministracionPutValidator()
        {
            RuleFor(i => i.ID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del registro a actualizar");

            RuleFor(i => i.Periodo)
                .NotEmpty().WithMessage("El {PropertyName} no puede ser vacio")
                .MaximumLength(100).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} carácteres");

            RuleFor(i => i.FechaInicio)
                .NotEmpty()
                    .WithMessage("La Fecha de Inicio no puede estar vacia")
                .LessThan(i => i.FechaTermino)
                    .WithMessage("La Fecha de Inicio no puede ser mayor a la Fecha de Termino");

            RuleFor(i => i.FechaTermino)
                .NotEmpty()
                    .WithMessage("La fecha de termino no puede estar vacia")
                .GreaterThan(i => i.FechaInicio)
                    .WithMessage("La Fecha de Termino no puede ser menor que la Fecha de Inicio");

            RuleFor(i => i.Estatus)
                .IsInEnum().WithMessage("El valor {PropertyValue} para {PropertyName} no es un valor válido");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el nombre del usuario que ejecuta la acción")
                .MaximumLength(25).WithMessage("El nombre del usuario, no puede ser mayor a {Maxlength} carácteres");
        }
    }
}
