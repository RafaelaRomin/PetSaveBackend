using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Services.Interfaces;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Application.Services.v1;

public class UserService (IUserRepository userRepository) : IUserService
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await userRepository.GetAllAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await userRepository.GetById(id);
        if (user is null) 
            throw new Exception("Usuário não encontrado!");
        
        return user;
    }

    public async Task<User> CreateAsync(UserInputModel inputModel)
    {
        var user = User.Create(
            inputModel.FullName, 
            inputModel.City, 
            inputModel.State, 
            inputModel.Email,
            inputModel.PhoneNumber,
            inputModel.Password);
        
        await userRepository.AddAsync(user);

        return user;
    }

    public async Task<bool> UpdateAsync(Guid id, UserInputModel inputModel)
    {
        var user = await userRepository.GetById(id);

        if (user is null) 
            throw new Exception("Usuário não encontrado!");
        
        user.Update(inputModel.FullName, inputModel.City, inputModel.State, inputModel.Email, inputModel.PhoneNumber, inputModel.Password);

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

    public async Task<User?> AuthenticateAsync(string loginInputEmail, string loginInputPassword)
    {
        var user = await userRepository.GetByEmailAsync(loginInputEmail);

        return user ?? null;
    }
}