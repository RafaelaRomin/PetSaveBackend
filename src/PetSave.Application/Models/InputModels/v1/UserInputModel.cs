namespace PetSave.Application.Models.InputModels.v1;

public class UserInputModel
{
    public string FullName { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set;} = default!;
    public string PhoneNumber { get; set; } = default!;
}