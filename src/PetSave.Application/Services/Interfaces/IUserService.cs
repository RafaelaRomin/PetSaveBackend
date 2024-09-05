using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Models.ViewModels.v1;
using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserViewModel>> GetAllAsync();
    Task<UserViewModel> GetByIdAsync(Guid id);
    Task<UserViewModel> CreateAsync(UserInputModel inputModel);
    Task<bool> UpdateAsync(Guid id, UserUpdateInputModel inputModel);
    Task<bool> DeleteAsync(Guid id);
    Task<LoginUserViewModel> GetUserByEmailAndPasswordAsync(LoginUserInputModel loginUserInputModel);
}