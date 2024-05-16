using System.ComponentModel.DataAnnotations.Schema;

namespace PetSave.Domain.Entities.v1;

public class PetDonation(
    Guid idPet, 
    DateTime donationDate
    ) : BaseEntity
{
    public Guid IdPet { get; private set; } = idPet;
    public DateTime DonationDate { get; private set; } = donationDate;
    public Pet? Pet { get; private set; }
}