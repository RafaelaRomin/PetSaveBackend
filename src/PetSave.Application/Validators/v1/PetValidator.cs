using FluentValidation;
using FluentValidation.Validators;
using PetSave.Application.Models.InputModels.v1;
using PetSave.Domain.Enums.v1;

namespace PetSave.Application.Validators.v1;

public class PetValidator : AbstractValidator<PetInputModel>
{

}