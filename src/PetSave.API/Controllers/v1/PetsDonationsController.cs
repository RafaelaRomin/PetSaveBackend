using Microsoft.AspNetCore.Mvc;
using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Services.Interfaces;

namespace PetSave.API.Controllers.v1;

[ApiController]
[Route("pets-donations")]
public class PetsDonationsController (
    IPetDonationService petDonationService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var donations = await petDonationService.GetAll();
        
        return Ok(donations);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var donation = await petDonationService.GetByIdAsync(id);

        return Ok(donation);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(PetDonationInputModel inputModel)
    {
        var donation = await petDonationService.CreateAsync(inputModel);

        return CreatedAtAction(nameof(GetById), new { id = donation }, donation);
    }
}