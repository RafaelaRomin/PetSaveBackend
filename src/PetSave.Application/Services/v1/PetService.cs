using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Services.Interfaces;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Application.Services.v1;

public class PetService(IPetRepository petRepository) : IPetService
{
    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await petRepository.GetAllAsync();
    }

    public async Task<Pet> GetByIdAsync(Guid id)
    {
        var pet = await petRepository.GetById(id);

        if (pet is null)
            throw new Exception("não encontrado o Id");

        return pet;
    }

    public async Task<Pet> CreateAsync(PetInputModel inputModel)
    {
        var pet = Pet.Create(
            inputModel.Name, 
            inputModel.Race, 
            inputModel.Species, 
            inputModel.Weight, 
            inputModel.Age,
            inputModel.Description, 
            inputModel.IdTutor);

        await petRepository.AddAsync(pet);

        return pet;
    }

    public async Task<Pet> UpdateAsync(Guid id, PetInputModel inputModel)
    {
        var pet = await petRepository.GetById(id);

        if (pet is null)
            throw new Exception("não encontrado o Id");
        
        pet.Update(inputModel.Name, inputModel.Race, inputModel.Weight, inputModel.Age, inputModel.Description);

        await petRepository.UpdateAsync(pet);

        return pet;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pet = await petRepository.GetById(id);

        if (pet is null)
            throw new Exception("não encontrado o Id");

        await petRepository.DeleteAsync(pet);

        return true;
    }
}