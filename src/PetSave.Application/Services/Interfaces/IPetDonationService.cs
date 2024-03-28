using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Models.ViewModels.v1;
using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Services.Interfaces;

public interface IPetDonationService
{
    Task<List<PetDonationViewModel>> GetAll();
    Task<PetDonationViewModel?> GetByIdAsync(Guid id);
    Task<PetDonationViewModel> CreateAsync(PetDonationInputModel inputModel);
}