using FeedsWebApi.Enums;

namespace FeedsWebApi.Dtos;

public class FeedResponseDto
{
    public int? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public FeedType Type { get; set; }

    public int? AuthorId { get; set; }

    public string? Author { get; set; }

    public string? VideoUrl { get; set; }

    public DateTime? PublishingDate { get; set; }

    public bool HasImage { get; set; }

    public int LikeCount { get; set; }
}
