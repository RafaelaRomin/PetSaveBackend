using Microsoft.EntityFrameworkCore;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Infra.Persistence.Repositories.v1;

public class UserRepository(PetSaveDbContext dbContext) : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User> GetById(Guid id)
    {
        return await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        dbContext.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        dbContext.Remove(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}