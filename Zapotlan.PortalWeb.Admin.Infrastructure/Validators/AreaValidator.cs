using FluentValidation;
using Zapotlan.PortalWeb.Admin.Core.DTOs;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Validators
{
    public class AreaPostValidator : AbstractValidator<AreaAddDto>
    {
        public AreaPostValidator() {
            RuleFor(i => i.UsuarioActualizacionID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del usuario que ejecuta la aplicación");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el Nombre del Usuario que ejecuta la aplicación")
                .MaximumLength(50).WithMessage("El Nombre del Usuario no puede ser mayor a {MaxLength} carácteres");
        }
    }

    public class AreaPutValidator : AbstractValidator<AreaEditDto>
    {
        public AreaPutValidator()
        {
            RuleFor(i => i.ID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del registro a actualizar");

            RuleFor(i => i.Clave)
                .MaximumLength(20).WithMessage("La {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.Nombre)
                .NotEmpty()
                .MaximumLength(100).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.NombreCorto)
                .MaximumLength(50).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.Descripcion)
                .MaximumLength(255).WithMessage("El {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.Tipo)
                .IsInEnum().WithMessage("El valor {PropertyValue} para {PropertyName} no es un valor válido");

            RuleFor(i => i.UbicacionDescripcion)
                .MaximumLength(500).WithMessage("La {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.Tags)
                .MaximumLength(400).WithMessage("Los {PropertyName} no puede ser mayor a {MaxLength} caracteres");

            RuleFor(i => i.Estatus)
                .IsInEnum().WithMessage("El valor {PropertyValue} para {PropertyName} no es un valor válido");

            RuleFor(i => i.UsuarioActualizacionID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del usuario que ejecuta la aplicación");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el Nombre del Usuario que ejecuta la aplicación")
                .MaximumLength(50).WithMessage("El Nombre del Usuario no puede ser mayor a {MaxLength} caracteres");

        }
    }

    public class AreaDeleteValidator : AbstractValidator<AreaDelDto>
    {
        public AreaDeleteValidator()
        {
            RuleFor(i => i.ID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del registro a dar de baja o eliminar");

            RuleFor(i => i.UsuarioActualizacionID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del usuario que ejecuta la aplicación");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el Nombre del Usuario que ejecuta la aplicación")
                .MaximumLength(50).WithMessage("El Nombre del Usuario no puede ser mayor a {MaxLength} caracteres");
        }
    }
}
