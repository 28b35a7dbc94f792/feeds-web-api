using FeedsWebApi.Models;

public class Like
{
    public int UserId { get; set; }    
    public User User { get; set; } = null!;

    public int FeedId { get; set; }
    public Feed Feed { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
