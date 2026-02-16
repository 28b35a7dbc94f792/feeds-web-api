using FeedsWebApi.Enums;

namespace FeedsWebApi.Models;

public class Feed
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int PublishingUserId { get; set; }
    public User PublishingUser { get; set; } = null!;

    public FeedType Type { get; set; }

    public byte[]? ImageData { get; set; }

    public string? ImageContentType { get; set; }

    public string? VideoUrl { get; set; }

    public List<Like> Likes { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
