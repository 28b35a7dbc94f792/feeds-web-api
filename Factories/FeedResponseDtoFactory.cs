using FeedsWebApi.Dtos;
using FeedsWebApi.Enums;
using FeedsWebApi.Models;

namespace FeedsWebApi.Factories;

public interface IFeedResponseDtoFactory
{
    FeedResponseDto Create(Feed feed);
    FeedResponseDto Create(
        string? title,
        string? description,
        string? author,
        DateTime? createdAt);
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
            AuthorId = feed.AuthorId,
            Author = feed.Author.Name ?? feed.Author.Username,
            Type = feed.Type,
            VideoUrl = feed.VideoUrl,
            PublishingDate = feed.PublishingDate,
            HasImage = feed.ImageData != null,
            LikeCount = feed.Likes.Count()
        };
    }

    public FeedResponseDto Create(
        string? title,
        string? description,
        string? author,
        DateTime? createdAt)
    {
        return new FeedResponseDto
        {
            Title = title ?? string.Empty,
            Description = description ?? string.Empty,
            Author = author,
            Type = FeedType.Text,
            PublishingDate = createdAt
        };
    }
}
