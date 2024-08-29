using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Models.ViewModels.MappingViewModels;
using PetSave.Application.Models.ViewModels.v1;
using PetSave.Application.Services.Interfaces;
using PetSave.Application.Validators.v1;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Application.Services.v1;

public class PetService(IPetRepository petRepository) : IPetService
{
    public async Task<IEnumerable<PetViewModel>> GetAllAsync(string? filter)
    {
        
        var pets = await petRepository.GetAllAsync(filter);

        var filteredPets = pets.Where(pet => pet.Status != DonationStatus.Unable);
        
        var petViewModels = filteredPets.ConvertPetsViewModel();

        return petViewModels;
        
    }

    public async Task<PetViewModel> GetByIdAsync(Guid id)
    {
        var pet = await petRepository.GetById(id);
        
        if (pet is null)
            throw new Exception("não encontrado o Id");
    
        var petViewModels = pet.ConvertPetViewModel();
    
        return petViewModels;
    }
    

    public async Task<IEnumerable<PetViewModel>> GetByTutorIdAsync(Guid tutorId)
    {
        var pets = await petRepository.GetByTutorIdAsync(tutorId);

        if (pets == null || !pets.Any())
            throw new Exception("No pets found for the given tutor ID");

        var petViewModels = pets.ConvertPetsViewModel();

        return petViewModels;
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

    public async Task<PetViewModel> UpdateAsync(Guid id, PetInputModel inputModel)
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

        var petViewModel = pet.ConvertPetViewModel();

        return petViewModel;
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