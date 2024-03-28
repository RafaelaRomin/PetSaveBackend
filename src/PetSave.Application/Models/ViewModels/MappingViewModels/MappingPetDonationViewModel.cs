using PetSave.Application.Models.ViewModels.v1;
using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Models.ViewModels.MappingViewModels;

public static class MappingPetDonationViewModel
{
    public static IEnumerable<PetDonationViewModel> ConvertPetDonationsViewModel(
        this IEnumerable<PetDonation> petDonations)
    {
        return (from petDonation in petDonations
            select new PetDonationViewModel
            {
                NamePet = petDonation.Pet.Name,
                DonationDate = petDonation.DonationDate.ToString("dd/MM/yyy"),
                StatusPet = petDonation.Pet.Status.ToString(),
                NameTutor = petDonation.Pet.Tutor.FullName
                    
            }).ToList();
    }

    public static PetDonationViewModel ConvertPetDonationViewModel(this PetDonation petDonation)
    {
        return new PetDonationViewModel
        {
            NamePet = petDonation.Pet.Name,
            DonationDate = petDonation.DonationDate.ToString("dd/MM/yyy"),
            StatusPet = petDonation.Pet.Status.ToString(),
            NameTutor = petDonation.Pet.Tutor.FullName
        };
    }
}
