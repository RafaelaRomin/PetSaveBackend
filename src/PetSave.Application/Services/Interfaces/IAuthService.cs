namespace PetSave.Infra.Auth;

public interface IAuthService
{
    string GenerateJWTToken(string email);
    string ComputeSha256Hash(string password);
}