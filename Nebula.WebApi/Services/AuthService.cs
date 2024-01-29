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

    public async Task<string> GenerateTokenAsync(string email, string password)
    {
        Customer customer;
        var user = await userRepository.SelectAsync(x => x.Email.Equals(email));
        if (user is null)
            customer = await customerRepository.SelectAsync(x => x.Email.Equals(email))
                ?? throw new NotFoundException("This user is not found!");

        else
        {
            return GenerateTokenForUser(user.Id.ToString(), user.UserRole.ToString(), user.Email, password, user.Password, user.Salt);
        }

        bool varifiedPassword = PasswordHasher.Verify(password, customer.Password, customer.Salt);
        if (!varifiedPassword)
            throw new CustomException(400, "Password or Email is incorrect!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Email", customer.Email),
                new Claim("Id", customer.Id.ToString()),
                new Claim("Status", "Customer")
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;

    }

    private string GenerateTokenForUser(string Id, string userRole, string email,string password, string innerPassword, string salt)
    {
        bool varifiedPassword = PasswordHasher.Verify(password, innerPassword, salt);
        if (!varifiedPassword)
            throw new CustomException(400, "Password or Email is incorrect!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Email", email),
                new Claim("Id", Id),
                new Claim(ClaimTypes.Role, userRole),
                new Claim("Status", "User")
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;
    }
};

public class TokenModel
{
    public string AccessToken { get; set; } = default!;
}
