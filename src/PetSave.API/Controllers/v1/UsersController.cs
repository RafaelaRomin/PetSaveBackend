using Microsoft.AspNetCore.Mvc;
using PetSave.Application.Models.InputModels.v1;
using PetSave.Application.Services.Interfaces;

namespace PetSave.API.Controllers.v1;

[ApiController]
[Route("v1/users")]
public class UsersController (IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await userService.GetAllAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userService.GetByIdAsync(id);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserInputModel inputModel)
    {
        var user = await userService.CreateAsync(inputModel);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, UserInputModel inputModel)
    {
        await userService.UpdateAsync(id, inputModel);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userService.DeleteAsync(id);

        return NoContent();
    }
}