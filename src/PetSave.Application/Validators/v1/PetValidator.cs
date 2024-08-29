using FluentValidation;
using PetSave.Application.Models.InputModels.v1;
using PetSave.Domain.Enums.v1;

namespace PetSave.Application.Validators.v1;

public class PetValidator : AbstractValidator<PetInputModel>
{
    public PetValidator()
    {

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .WithMessage("O peso do pet deve ser maior que zero.");

        RuleFor(x => x.Age)
            .GreaterThan(0)
            .WithMessage("A idade do pet deve ser maior que zero.");

        When(x => x.Species == Species.Canine, () =>
        {
            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(25)
                .WithMessage("Caninos só podem doar com mais de 25kg.");

            RuleFor(x => x.Age)
                .InclusiveBetween(1.6, 7)
                .WithMessage("Caninos precisam ter entre 1,6 à 7 anos para realizar doações.");
        });

        When(x => x.Species == Species.Feline, () =>
        {
            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(4)
                .WithMessage("Felinos precisam pesar no mínimo 4kg.");

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 7)
                .WithMessage("Felinos precisam ter idade entre 1 e 7 anos.");
        });
    }
}