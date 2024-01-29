using Nebula.WebApi.Services;
using System.Text.Json;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("user/login")]
    public async Task<IActionResult> GenerateTokenAsync(string email, string password)
    {
        var token = await authService.GenerateTokenAsync(email, password);

        var result = JsonSerializer.Serialize(new TokenModel { AccessToken = token });
       
        return Ok(result);
    }
}
