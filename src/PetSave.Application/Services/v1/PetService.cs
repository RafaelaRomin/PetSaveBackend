using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Services.Interfaces;
using PetSave.Application.Validators.v1;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Application.Services.v1;

public class PetService(IPetRepository petRepository) : IPetService
{
    public async Task<IEnumerable<Pet>> GetAllAsync(int? specie)
    {
        var pets = await petRepository.GetAllAsync(specie);

        var filteredPets = pets.Where(pet => pet.Status != DonationStatus.Unable);

        return filteredPets;
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
        var validator = new PetValidator();
        
        var result = validator.Validate(inputModel);
        
        if(! result.IsValid) 
        {
            foreach(var failure in result.Errors)
            {
                throw new Exception("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            }
        }
        
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
        var validator = new PetValidator();
        
        var result = validator.Validate(inputModel);
        
        if(! result.IsValid) 
        {
            foreach(var failure in result.Errors)
            {
                throw new Exception("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            }
        }
        
        var pet = await petRepository.GetById(id);

        if (pet is null)
            throw new Exception("Pet não encontrado");
        
        pet.Update(inputModel.Name, inputModel.Race, inputModel.Weight, inputModel.Age, inputModel.Description);

        await petRepository.UpdateAsync(pet);

        return pet;
    }

    public async Task<bool> ChangeStatus(Guid id, DonationStatus status)
    {
        var pet = await petRepository.GetById(id);

        if (pet is null)
            throw new Exception("Pet não encontrado");

        pet.Status = status;

        await petRepository.UpdateAsync(pet);

        return true;
    }

    public async Task<bool> TemporarilyUnavailable(Guid id, int daysUnavailable)
    {
        var pet = await petRepository.GetById(id);

        if (pet is null)
            throw new Exception("Pet não encontrado");
        
        pet.UpdateUnableStatus();

        await petRepository.UpdateAsync(pet);

        await ScheduleStatusChange(pet.Id, TimeSpan.FromDays(daysUnavailable));

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pet = await petRepository.GetById(id);

        if (pet is null)
            throw new Exception("não encontrado o Id");

        await petRepository.DeleteAsync(pet);

        return true;
    }
    
    
    private async Task ScheduleStatusChange(Guid petId, TimeSpan delay)
    {
        await Task.Delay(delay);
        await ChangeStatus(petId, DonationStatus.Available);
    }
}