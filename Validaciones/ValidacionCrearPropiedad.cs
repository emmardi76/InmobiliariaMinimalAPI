using FluentValidation;
using InmobiliariaMinimalAPI.Modelos.DTOS;

namespace InmobiliariaMinimalAPI.Validaciones;

public class ValidacionCrearPropiedad : AbstractValidator<CrearPropiedadDTO>
{
    public ValidacionCrearPropiedad()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");
        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es obligatoria.")
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
        RuleFor(x => x.Ubicacion).NotEmpty().WithMessage("La ubicación es obligatoria.");
    }
}
