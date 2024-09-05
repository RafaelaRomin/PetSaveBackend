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
                Id = pet.Id,
                Name = pet.Name,
                Race = pet.Race,
                Age = pet.Age,
                Specie = pet.Species.ToString(),
                Weight = pet.Weight,
                Description = pet.Description,
                DonationStatus = pet.Status.ToString(),
                TutorName = pet.Tutor.FullName,
                TutorPhoneNumber = pet.Tutor.PhoneNumber
            }).ToList();
    }

    public static PetViewModel ConvertPetViewModel(this Pet pet)
    {
        return new PetViewModel
        {
            Id = pet.Id,
            Name = pet.Name,
            Race = pet.Race,
            Age = pet.Age,
            Specie = pet.Species.ToString(),
            Weight = pet.Weight,
            Description = pet.Description,
            DonationStatus = pet.Status.ToString(),
            TutorName = pet.Tutor.FullName,
            TutorPhoneNumber = pet.Tutor.PhoneNumber
        };
    }
}