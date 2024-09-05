using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Models.ViewModels.MappingViewModels;
using PetSave.Application.Models.ViewModels.v1;
using PetSave.Application.Services.Interfaces;
using PetSave.Application.Validators.v1;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Repositories.Interfaces;
using PetSave.Infra.Auth;

namespace PetSave.Application.Services.v1;

public class UserService (IUserRepository userRepository, IAuthService authService) : IUserService
{
    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        
        var usersList = await userRepository.GetAllAsync();
        
        var userViewModel = usersList.ConvertUsersToViewModel();
        
        return userViewModel;
    }

    public async Task<UserViewModel> GetByIdAsync(Guid id)
    {
        var user = await userRepository.GetById(id);
        if (user is null) 
            throw new Exception("Usuário não encontrado!");
        
        var userViewModel = user.ConvertUserToViewModel();
        
        return userViewModel;
    }

    public async Task<UserViewModel> CreateAsync(UserInputModel inputModel)
    {
        var validator = new UserValidator();
        
        var hashPassword = authService.ComputeSha256Hash(inputModel.Password);
        
        
        
        var user = User.Create(
            inputModel.FullName, 
            inputModel.City, 
            inputModel.State, 
            inputModel.Email,
            inputModel.PhoneNumber,
            hashPassword);

        var result = validator.Validate(inputModel);

        if (!result.IsValid)
        {
            foreach (var failure in result.Errors)
            {
                throw new Exception("Property " + failure.PropertyName + " failed validation. Error was: " +
                                    failure.ErrorMessage);
            }
        }

        await userRepository.AddAsync(user);

        var userViewModel = user.ConvertUserToViewModel();
        
        return userViewModel;
        
    }

    public async Task<bool> UpdateAsync(Guid id, UserUpdateInputModel inputModel)
    {
        
        var user = await userRepository.GetById(id);

        if (user is null) 
            throw new Exception("Usuário não encontrado!");
        
        
        user.Update(
            inputModel.FullName ?? user.FullName, 
            inputModel.City ?? user.City, 
            inputModel.State ?? user.State, 
            inputModel.Email ?? user.Email, 
            inputModel.PhoneNumber ?? user.PhoneNumber
            );

        await userRepository.UpdateAsync(user);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await userRepository.GetById(id);

        if (user is null) 
            throw new Exception("Usuário não encontrado!");

        await userRepository.DeleteAsync(user);

        return true;
    }

    public async Task<LoginUserViewModel> GetUserByEmailAndPasswordAsync(LoginUserInputModel loginUserInputModel)
    {

        var passwordHash = authService.ComputeSha256Hash(loginUserInputModel.Password);
        
        var user = await userRepository.GetUserByEmailAndPasswordAsync(loginUserInputModel.Email, passwordHash);

        var token = authService.GenerateJWTToken(user.Email) ;
        
        return new LoginUserViewModel
        {   
            Email = user.Email,
            Name = user.FullName,
            Token = token,
            Id = user.Id
        };
    }
}