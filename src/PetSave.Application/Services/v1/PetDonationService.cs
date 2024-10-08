using FluentValidation;
using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Models.ViewModels.MappingViewModels;
using PetSave.Application.Models.ViewModels.v1;
using PetSave.Application.Services.Interfaces;
using PetSave.Application.Validators.v1;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;
using PetSave.Domain.Repositories.Interfaces;

namespace PetSave.Application.Services.v1;

public class PetDonationService (
    IPetDonationRepository petDonationRepository, 
    IPetRepository petRepository,
    IPetService petService)
    : IPetDonationService
{
    public async Task<List<PetDonationViewModel>> GetAll()
    {
        var donations = await petDonationRepository.GetAllAsync();

        var donationsViewModel = donations.ConvertPetDonationsViewModel();

        return donationsViewModel.ToList();
    }

    public async Task<PetDonationViewModel?> GetByIdAsync(Guid id)
    {
        var donation = await petDonationRepository.GetByIdAsync(id);

        var donationViewModel = donation!.ConvertPetDonationViewModel();
        
        return donationViewModel;
    }

    public async Task<PetDonationViewModel> CreateAsync(PetDonationInputModel inputModel)
    {
        
        var pet = await petRepository.GetById(inputModel.IdPet);
        

        if (pet is null)
            throw new Exception("Pet não encontrado");
        
        if (pet.Status == DonationStatus.Unable)
            throw new Exception("Pet não está apto para doação");
        

        var donation = new PetDonation(
            inputModel.IdPet, 
            inputModel.DonationDate);
        
        
        await petDonationRepository.CreateAsync(donation);

        await petService.ChangeStatus(pet.Id, DonationStatus.Unable);
        
        ScheduleStatusChange(pet.Id, TimeSpan.FromDays(90));

        var donationDb = await petDonationRepository.GetByIdAsync(donation.Id);
        
        if(donationDb is null)
            throw new Exception("Doação não encontrada!");

        var donationViewModel = donationDb.ConvertPetDonationViewModel();
        
        return donationViewModel;
    }
    
    private void ScheduleStatusChange(Guid petId, TimeSpan delay)
    {
        Task.Run(async () =>
        {
            await Task.Delay(delay);
            await petService.ChangeStatus(petId, DonationStatus.Available);
        });
    }
    
}