using FluentValidation;
using PetSave.Application.Models.InputModels.v1;

namespace PetSave.Application.Validators.v1;

public class UserValidator : AbstractValidator<UserInputModel>
{
    public UserValidator()
    {
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email é um campo obrigatório!");

        RuleFor(user => user.Email)
            .EmailAddress()
            .WithMessage("Formato de email inválido!");
        
        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .WithMessage("O campo telefone é obrigatório!");
        
        RuleFor(user => user.PhoneNumber)
            .Matches(@"^(?:\(\d{2}\)\s9\d{4}-\d{4}|\d{4}-\d{4}|\d{2}\s9\d{4}-\d{4}|55\d{11})$")
            .WithMessage("Número invalido de telefone!");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("O campo senha é obrigatório!")
            .MinimumLength(8)
            .WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
            .WithMessage("A senha deve ter pelo menos 8 caracteres, conter pelo menos uma letra maiúscula, um número e um caractere especial.");
        
    }
}