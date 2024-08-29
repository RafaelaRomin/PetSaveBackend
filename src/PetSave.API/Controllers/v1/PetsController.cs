using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Models.ViewModels.v1;
using PetSave.Application.Services.Interfaces;

namespace PetSave.API.Controllers.v1;

[ApiController]
[Route("v1/pets")]
public class PetsController(IPetService petService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetByFilter([FromQuery] string? filter)
    {
        var pets = await petService.GetAllAsync(filter);

        return Ok(pets);
    }

    [HttpGet("by-tutor/{tutorId}")]
    public async Task<IActionResult> GetByTutorId(Guid tutorId)
    {
        var pets = await petService.GetByTutorIdAsync(tutorId);

        return Ok(pets);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var pet = await petService.GetByIdAsync(id);

        return Ok(pet);
    }

    [HttpPost]
    public async Task<IActionResult> Post(PetInputModel inputModel)
    {
        var pet = await petService.CreateAsync(inputModel);

        return Ok(pet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, PetInputModel inputModel)
    {
        await  petService.UpdateAsync(id, inputModel);
        
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> TemporarilyUnavailable(Guid petId, int daysUnavailable)
    {
        await petService.TemporarilyUnavailable(petId, daysUnavailable);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await petService.DeleteAsync(id);

        return NoContent();
    }
}