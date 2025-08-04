namespace WebApi.Models;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; } = null!;
    
    public string? Name { get; set; }
    
    // Parol
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public ICollection<Message> Messages { get; set; }
}
