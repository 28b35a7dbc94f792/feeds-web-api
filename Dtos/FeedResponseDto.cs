namespace FeedsWebApi.Dtos;

public class FeedResponseDto : FeedBaseDto
{
    public int? Id { get; set; }

    public int? AuthorId { get; set; }

    public string? Author { get; set; }

    public DateTime? PublishingDate { get; set; }

    public bool HasImage { get; set; }

    public int LikeCount { get; set; }
}
