namespace FeedsWebApi.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Name { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
