using FluentValidation;
using Zapotlan.PortalWeb.Admin.Core.DTOs;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Validators
{
    public class PersonaPostValidator : AbstractValidator<PersonaAddDto>
    {
        public PersonaPostValidator()
        {
            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el nombre del usuario que ejeucta la acción.")
                .MaximumLength(50).WithMessage("El nombre del usuario, no puede ser mayor a {Maxlength} carácteres");
        }
    }

    public class PersonaPutValidator : AbstractValidator<PersonaEditDto> 
    {
        public PersonaPutValidator()
        {
            // https://es.stackoverflow.com/questions/31039/c%C3%B3mo-validar-una-curp-de-m%C3%A9xico
            //string patronRFC = @"^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]*$";
            string patronRFC = @"^(([A-ZÑ&]{4})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
                "(([A-ZÑ&]{4})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
                "(([A-ZÑ&]{4})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
                "(([A-ZÑ&]{4})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$";
            string patronCURP = @"^([A-ZÑ][AEIOUXÁÉÍÓÚ][A-ZÑ]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$";

            RuleFor(i => i.ID)
                .NotEmpty().WithMessage("Faltó especificar el identificador del registro a actualizar");

            RuleFor(i => i.PrimerApellido)
                .NotEmpty().When(i => string.IsNullOrEmpty(i.SegundoApellido)).WithMessage("Al menos debe de haber un apellido.")
                .MaximumLength(100).WithMessage("El primer apellido, no puede ser mayor a {Maxlength} carácteres"); ;

            RuleFor(i => i.SegundoApellido)
                .NotEmpty().When(i => string.IsNullOrEmpty(i.PrimerApellido)).WithMessage("Al menos debe de haber un apellido.")
                .MaximumLength(100).WithMessage("El segundo apellido, no puede ser mayor a {Maxlength} carácteres"); ;

            RuleFor(i => i.CURP)
                .Matches(patronCURP)
                .When(i => !string.IsNullOrEmpty(i.CURP))
                .WithMessage("El CURP tiene un valor no válido");

            RuleFor(i => i.RFC)
                .Matches(patronRFC)
                .When(i => !string.IsNullOrEmpty(i.RFC))
                .WithMessage("La RFC tiene un valor no válido");

            RuleFor(i => i.FechaDefuncion)
                .GreaterThan(DateTime.Today)
                .WithMessage("La fecha de defunción, no puede ser mayor a la del día de hoy.");

            RuleFor(i => i.EstadoVida)
                .IsInEnum().WithMessage("El valor {PropertyValue} para {PropertyName} no es un valor válido");

            RuleFor(i => i.Estatus)
                .IsInEnum().WithMessage("El valor {PropertyValue} para {PropertyName} no es un valor válido");

            RuleFor(i => i.UsuarioActualizacion)
                .NotEmpty().WithMessage("Faltó especificar el nombre del usuario que ejecuta la acción")
                .MaximumLength(50).WithMessage("El nombre del usuario, no puede ser mayor a {Maxlength} carácteres");
        }
    }
}
