using PetSave.Domain.Entities.v1;

namespace PetSave.Domain.Repositories.Interfaces;

public interface IPetDonationRepository
{
    Task<List<PetDonation?>> GetAllAsync();
    Task<PetDonation?> GetByIdAsync(Guid id);
    Task CreateAsync(PetDonation? donation);
}