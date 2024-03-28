namespace PetSave.Application.Models.InputModels.v1;

public class LoginUserInputModel
{
    public string Email { get; set; } = default!;
    public string Password { get; set;} = default!;
}