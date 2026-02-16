using FeedsWebApi.Dtos;
using FeedsWebApi.Models;

namespace FeedsWebApi.Factories;

public interface IFeedResponseDtoFactory
{
    FeedResponseDto Create(Feed feed);
}

public class FeedResponseDtoFactory : IFeedResponseDtoFactory
{
    public FeedResponseDto Create(Feed feed)
    {
        return new FeedResponseDto
        {
            Id = feed.Id,
            Title = feed.Title,
            Description = feed.Description,
            PublishingUserId = feed.PublishingUserId,
            Type = feed.Type,
            VideoUrl = feed.VideoUrl,
            CreatedAt = feed.CreatedAt,
            LikeCount = feed.Likes.Count()
        };
    }
}
