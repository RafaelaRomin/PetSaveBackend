using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PetSave.Infra.Auth;

public class AuthService(IConfiguration configuration) : IAuthService
{
    
    private readonly IConfiguration _configuration = configuration;

    public string GenerateJWTToken(string email)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var key = configuration["Jwt:key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); ;

        var claims = new List<Claim>
        { 
            new(nameof(ClaimTypes.Email), email)
        };

        var token = new JwtSecurityToken(
            issuer: issuer, 
            audience: audience, 
            expires: DateTime.Now.AddHours(8), 
            signingCredentials: credentials, 
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
    public string ComputeSha256Hash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

    }
}