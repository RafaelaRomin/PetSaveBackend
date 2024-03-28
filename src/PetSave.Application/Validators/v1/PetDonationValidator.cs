using FluentValidation;
using PetSave.Application.Models.InputModels.v1;

namespace PetSave.Application.Validators.v1;

public class PetDonationValidator : AbstractValidator<PetDonationInputModel>
{
    public PetDonationValidator()
    {
        
    }
}