namespace Nebula.Application.Interfaces;

public interface IAuthService
{
    Task<string> GenerateTokenForUserAsync(string email, string password);
    Task<string> GenerateTokenForCustomerAsync(string email, string password);
}
