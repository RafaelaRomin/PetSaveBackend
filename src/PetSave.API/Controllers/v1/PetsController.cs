using Microsoft.AspNetCore.Mvc;
using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Services.Interfaces;

namespace PetSave.API.Controllers.v1;

[ApiController]
[Route("api/pets")]
public class PetsController(IPetService petService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var pets = await petService.GetAllAsync();

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

        return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, PetInputModel inputModel)
    {
        await  petService.UpdateAsync(id, inputModel);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await petService.DeleteAsync(id);

        return NoContent();
    }
}