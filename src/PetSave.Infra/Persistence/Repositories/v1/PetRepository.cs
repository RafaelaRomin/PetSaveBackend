using Microsoft.EntityFrameworkCore;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Infra.Persistence.Repositories.v1;

public class PetRepository(PetSaveDbContext dbContext) : IPetRepository
{
    public async Task<List<Pet>> GetAllAsync(string? filter)
    {
        IQueryable<Pet> query = dbContext.Pets.Include(p => p.Tutor);

        var pets = await query.ToListAsync();

        if (!string.IsNullOrEmpty(filter))
        {
            pets = pets.Where(p =>
                p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                p.Species.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                p.Weight.ToString().Contains(filter) ||
                p.LastDonation.ToString("dd/MM/yyyy").Contains(filter) ||
                p.Status.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                p.Tutor.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                p.Tutor.PhoneNumber.Contains(filter)
            ).ToList();
        }

        return pets;
    }
    
    public async Task<Pet> GetById(Guid id)
    {
        return await dbContext.Pets
            .Include(p => p.Tutor)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pet>> GetByTutorIdAsync(Guid tutorId)
    {
        return await dbContext.Pets.Include(p => p.Tutor)
            .Where(p => p.Tutor.Id == tutorId)
            .ToListAsync();
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