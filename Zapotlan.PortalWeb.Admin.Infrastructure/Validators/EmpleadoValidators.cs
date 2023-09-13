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

            RuleFor(i => i.Codigo)
                .NotEmpty().WithMessage("Faltó especifiar el Código del Empleado")
                .MaximumLength(40).WithMessage("La {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.NombrePuesto)
                .MaximumLength(200).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.ArchivoFotografia)
                .MaximumLength(260).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.ArchivoCV)
                .MaximumLength(40).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.FechaIngreso)
                .NotEmpty().WithMessage("La fecha de ingreso no puede estar vacia");

            RuleFor(i => i.TipoNomina)
                .MaximumLength(100).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.Estatus)
                .IsInEnum().WithMessage("El valor {PropertyValue} para {PropertyName} no es un valor válido");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el nombre del usuario que ejecuta la acción")
                .MaximumLength(50).WithMessage("El nombre del usuario, no puede ser mayor a {Maxlength} carácteres");
        }
    }

    public class EmpleadoDeleteValidator : AbstractValidator<EmpleadoDelDto> 
    {
        public EmpleadoDeleteValidator()
        {
            RuleFor(i => i.ID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del registro a dar de baja o eliminar.");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el Nombre del Usuario que ejecuta la aplicación")
                .MaximumLength(50).WithMessage("El Nombre del Usuario no puede ser mayor a {MaxLength} caracteres");
        }
    }
}
