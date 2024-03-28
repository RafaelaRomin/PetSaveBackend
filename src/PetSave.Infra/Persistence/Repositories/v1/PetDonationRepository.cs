using Microsoft.EntityFrameworkCore;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Infra.Persistence.Repositories.v1;

public class PetDonationRepository(PetSaveDbContext dbContext) : IPetDonationRepository
{
    public async Task<List<PetDonation>> GetAllAsync()
    {
        return await dbContext.PetDonations.ToListAsync();
    }

    public IQueryable<PetDonation> GetByIdAsync(Guid id)
    {
        return dbContext.PetDonations.Where(d => d.Id == id);
    }

    public async Task CreateAsync(PetDonation donation)
    {
        await dbContext.PetDonations.AddAsync(donation);
        await dbContext.SaveChangesAsync();
    }
}