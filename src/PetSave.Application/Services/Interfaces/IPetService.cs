using PetSave.Application.Models.InputModels.v1;
using PetSave.Domain.Entities.v1;

namespace PetSave.Application.Services.Interfaces;

public interface IPetService
{
    Task<IEnumerable<Pet>> GetAllAsync(int? specie);
    Task<Pet> GetByIdAsync(Guid id);
    Task<Pet> CreateAsync(PetInputModel inputModel);
    Task<Pet> UpdateAsync(Guid id, PetInputModel inputModel);
    Task<bool> ChangeStatus(Guid id);
    Task<bool> DeleteAsync(Guid id);
}