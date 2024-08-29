using PetSave.Application.Models.ViewModels.v1;
using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Models.ViewModels.MappingViewModels;

public static class MappingPetViewModel
{
    public static IEnumerable<PetViewModel> ConvertPetsViewModel(this IEnumerable<Pet> pets)
    {
        return (from pet in pets
            select new PetViewModel
            {
                Name = pet.Name,
                Specie = pet.Species.ToString(),
                Weight = pet.Weight,
                DonationStatus = pet.Status.ToString(),
                TutorName = pet.Tutor.FullName,
                TutorPhoneNumber = pet.Tutor.PhoneNumber
            }).ToList();
    }

    public static PetViewModel ConvertPetViewModel(this Pet pet)
    {
        return new PetViewModel
        {
            Name = pet.Name,
            Specie = pet.Species.ToString(),
            Weight = pet.Weight,
            DonationStatus = pet.Status.ToString(),
            TutorName = pet.Tutor.FullName,
            TutorPhoneNumber = pet.Tutor.PhoneNumber
        };
    }
}