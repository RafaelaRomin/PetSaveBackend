using FluentValidation;
using PetSave.Application.Models.InputModels.v1;

namespace PetSave.Application.Validators.v1;

public class PetValidator : AbstractValidator<PetInputModel>
{
    public PetValidator()
    {
        
    }
}