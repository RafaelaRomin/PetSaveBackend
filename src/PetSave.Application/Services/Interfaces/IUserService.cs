using PetSave.Application.Models.InputModels.v1;
using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<User> CreateAsync(UserInputModel inputModel);
    Task<bool> UpdateAsync(Guid id, UserInputModel inputModel);
    Task<bool> DeleteAsync(Guid id);
    Task<User?> AuthenticateAsync(string loginInputEmail, string loginInputPassword);
}