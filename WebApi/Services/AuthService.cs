using WebApi.Dtos;
using WebApi.Models;
using WebApi.Utils;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (existing != null)
            throw new Exception("Username already taken");

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = PasswordHasher.Hash(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _jwtService.GenerateToken(user);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        return _jwtService.GenerateToken(user);
    }
}
