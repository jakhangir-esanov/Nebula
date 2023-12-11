using Microsoft.IdentityModel.Tokens;
using Nebula.Application.Helpers;
using Nebula.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nebula.WebApi.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;
    private readonly IRepository<User>  userRepository;
    private readonly IRepository<Customer> customerRepository;
    public AuthService(IConfiguration configuration, IRepository<User> userRepository, 
        IRepository<Customer> customerRepository)
    {
        this.configuration = configuration;
        this.userRepository = userRepository;
        this.customerRepository = customerRepository;
    }

    public async Task<string> GenerateTokenForUserAsync(string email, string password)
    {
        var user = await userRepository.SelectAsync(x => x.Email.Equals(email))
            ?? throw new NotFoundException("User not found!");

        bool varifiedPassword = PasswordHasher.Verify(password, user.Password, user.Salt);
        if (!varifiedPassword)
            throw new CustomException(400, "Password or Email is incorrect!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;
    }
}
