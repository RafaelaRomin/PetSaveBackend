using Microsoft.EntityFrameworkCore;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Infra.Persistence.Repositories.v1;

public class PetDonationRepository(PetSaveDbContext dbContext) : IPetDonationRepository
{
    public async Task<List<PetDonation?>> GetAllAsync()
    {
        return await dbContext.PetDonations
            .Include(p => p.Pet)
            .ThenInclude(t => t.Tutor)
            .ToListAsync();
    }

    public async Task<PetDonation?> GetByIdAsync(Guid id)
    {
        return await dbContext.PetDonations
            .Include(p => p.Pet)
            .ThenInclude(p => p.Tutor)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreateAsync(PetDonation? donation)
    {
        await dbContext.PetDonations.AddAsync(donation);
        await dbContext.SaveChangesAsync();
    }
}