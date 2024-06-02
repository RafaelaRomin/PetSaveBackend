using PetSave.Domain.Entities.v1;

namespace PetSave.Domain.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> GetById(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User> GetByEmailAsync(string email);
}