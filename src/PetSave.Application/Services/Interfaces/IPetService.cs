using PetSave.Application.Models.InputModels.v1;
using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;

namespace PetSave.Application.Services.Interfaces;

public interface IPetService
{
    Task<IEnumerable<Pet>> GetAllAsync(int? specie);
    Task<Pet> GetByIdAsync(Guid id);
    Task<Pet> CreateAsync(PetInputModel inputModel);
    Task<Pet> UpdateAsync(Guid id, PetInputModel inputModel);
    Task<bool> ChangeStatus(Guid id, DonationStatus status);
    Task<bool> TemporarilyUnavailable(Guid id, int daysUnavailable);
    Task<bool> DeleteAsync(Guid id);
}