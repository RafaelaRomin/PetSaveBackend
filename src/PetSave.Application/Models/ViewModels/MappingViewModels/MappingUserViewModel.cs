using PetSave.Application.Models.ViewModels.v1;
using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Models.ViewModels.MappingViewModels;

public static class MappingUserViewModel
{
    public static IEnumerable<UserViewModel> ConvertUsersToViewModel(
        this IEnumerable<User> users)
    {
        return (from user in users
            select new UserViewModel
            {
                FullName = user.FullName,
                City = user.City,
                State = user.State,
                PhoneNumber = user.PhoneNumber
            }).ToList();
    }

    public static UserViewModel ConvertUserToViewModel(this User user)
    {
        List<PetUserViewModel> list = [];
        foreach (var pet in user.Pets)
        {
            list.Add(new PetUserViewModel
            {
                Name = pet.Name,
                Specie = pet.Species.ToString(),
                Weight = pet.Weight,
                LastDonation = pet.LastDonation.ToString("dd/MM/yyyy"),
                DonationStatus = pet.Status.ToString()
            });
        }
        
        return new UserViewModel
        {
            FullName = user.FullName,
            City = user.City,
            State = user.State,
            PhoneNumber = user.PhoneNumber,
            Pets = list
        };
    }
}