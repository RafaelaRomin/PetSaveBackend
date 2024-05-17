using FluentValidation;
using PetSave.Application.Models.InputModels.v1;

namespace PetSave.Application.Validators.v1;

public class UserValidator : AbstractValidator<UserInputModel>
{
    public UserValidator()
    {
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email is required field.");

        RuleFor(user => user.Email)
            .EmailAddress()
            .WithMessage("Invalid email format");
        
        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");
        
        RuleFor(user => user.PhoneNumber)
            .Matches(@"^\+\d{1,3}\s?\d{1,14}(\s?\d{1,13})?$")
            .WithMessage("Invalid phone number format.");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
            .WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, one number, and one special character.");
        
    }
}