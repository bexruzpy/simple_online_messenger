namespace WebApi.Models;

public class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    // Group Owner - Foreign key bilan
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    // Group a'zolari (Many-to-Many bo'lishi mumkin)
    public ICollection<User> Users { get; set; } = new List<User>();

    // Groupdagi xabarlar
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
