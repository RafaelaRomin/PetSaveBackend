using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
    [AllowAnonymous]
    public async Task<IActionResult> Post(UserInputModel inputModel)
    {
        var user = await userService.CreateAsync(inputModel);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserInputModel loginInput)
    {
        var user = await userService.AuthenticateAsync(loginInput.Email, loginInput.Password);

        if (user == null)
            return Unauthorized();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("your_secret_key_here"); // Use a secure key and store it securely
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
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