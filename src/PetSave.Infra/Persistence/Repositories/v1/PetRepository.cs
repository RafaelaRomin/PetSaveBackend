using Microsoft.EntityFrameworkCore;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Infra.Persistence.Repositories.v1;

public class PetRepository(PetSaveDbContext dbContext) : IPetRepository
{
    public async Task<List<Pet>> GetAllAsync(int? specie)
    {
        IQueryable<Pet> query = dbContext.Pets;

        if (specie.HasValue)
        {
            query = query.Where(s => (int)s.Species == specie.Value);
        }
        return await query.ToListAsync();
    }

    public async Task<Pet?> GetById(Guid id)
    {
        return await dbContext.Pets.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Pet pet)
    {
        await dbContext.Pets.AddAsync(pet);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pet pet)
    {
        dbContext.Pets.Update(pet);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Pet pet)
    {
        dbContext.Remove(pet);
        await dbContext.SaveChangesAsync();
    }
}