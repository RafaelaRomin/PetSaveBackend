﻿using PetSave.Domain.Entities.v1;
using PetSave.Domain.Enums.v1;

namespace PetSave.Domain.Repositories.Interfaces;

public interface IPetRepository
{
    Task <List<Pet>> GetAllAsync(int? specie);
    Task<Pet?> GetById(Guid id);
    Task AddAsync(Pet pet);
    Task DeleteAsync(Pet pet);
    Task UpdateAsync(Pet pet);
}