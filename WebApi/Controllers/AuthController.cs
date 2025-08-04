using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // 🔐 Bu yerda siz bazadan foydalanuvchini tekshirishingiz kerak
        // Masalan: userService.Validate(request.Username, request.Password)

        var userId = 1; // Bu demo uchun, aslida DB dan olinadi
        var token = _authService.GenerateToken(userId);
        return Ok(new { token });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
