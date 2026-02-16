namespace FeedsWebApi.Dtos;

public class FeedResponseDto : FeedBaseDto
{
    public int Id { get; set; }

    public int PublishingUserId { get; set; }

    public int LikeCount { get; set; }
}
