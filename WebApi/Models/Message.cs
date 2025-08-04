namespace WebApi.Models;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public User User { get; set; }

    public int? GroupId { get; set; }
    public Group? Group { get; set; }
}
