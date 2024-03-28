using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Models.InputModels.v1;

public class PetDonationInputModel
{
    public Guid IdPet { get; set; }
    public DateTime DonationDate { get; set; }
}