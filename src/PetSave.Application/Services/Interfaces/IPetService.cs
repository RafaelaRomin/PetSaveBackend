using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Models.ViewModels.v1;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;

namespace PetSave.Application.Services.Interfaces;

public interface IPetService
{
    Task<IEnumerable<PetViewModel>> GetAllAsync(string? filter, Species? specieSelected);
    Task<PetViewModel> GetByIdAsync(Guid id);
    Task<IEnumerable<PetViewModel>> GetByTutorIdAsync(Guid tutorId);
    Task<Pet> CreateAsync(PetInputModel inputModel);
    Task<PetViewModel> UpdateAsync(Guid id, Guid tutorId, PetUpdateInputModel inputModel);
    Task<bool> ChangeStatus(Guid id, DonationStatus status);
    Task<bool> TemporarilyUnavailable(Guid id, int daysUnavailable);
    Task<bool> DeleteAsync(Guid id);
}