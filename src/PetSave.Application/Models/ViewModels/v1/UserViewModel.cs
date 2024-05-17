using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Models.ViewModels.v1;

public class UserViewModel
{
    public string FullName { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PhoneNumber { get; set; }
    public List<PetUserViewModel> Pets { get; set; }

}