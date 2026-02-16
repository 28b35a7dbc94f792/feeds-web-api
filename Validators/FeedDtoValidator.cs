using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using FeedsWebApi.Enums;

namespace FeedsWebApi.Validators;

public interface IFeedDtoValidator
{
    Task ValidateCreate(FeedCreateDto dto);
    Task ValidateUpdate(FeedUpdateDto dto);
}

public class FeedDtoValidator : IFeedDtoValidator
{
    private readonly AppDbContext _context;

    public FeedDtoValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task ValidateCreate(FeedCreateDto dto)
    {
        await ValidateCommon(dto);

        var publishingUser = await _context.Users.FindAsync(dto.PublishingUserId);

        if (publishingUser == null)
            throw new ArgumentException("Publishing User ID is invalid.");

        if (dto.Type == FeedType.Image && dto.Image == null)
            throw new ArgumentException("Image is required for image feeds.");

        if (dto.Type == FeedType.Text && dto.Image != null)
            throw new ArgumentException("Image is not allowed for text feeds.");
    }

    public async Task ValidateUpdate(FeedUpdateDto dto)
    {
        await ValidateCommon(dto);

        if (dto.Type == FeedType.Image && dto.Image == null)
            throw new ArgumentException("Image is required for image feeds.");

        if (dto.Type == FeedType.Text && dto.Image != null)
            throw new ArgumentException("Image is not allowed for text feeds.");
    }

    private async Task ValidateCommon(FeedBaseDto dto)
    {
        if (dto == null)
            throw new ArgumentException("Request cannot be null.");

        if (string.IsNullOrEmpty(dto.Title))
            throw new ArgumentException("Title is required.");

        if (string.IsNullOrEmpty(dto.Description))
            throw new ArgumentException("Description is required.");

        if (dto.Type == FeedType.Video && string.IsNullOrEmpty(dto.VideoUrl))
            throw new ArgumentException("Video URL is required for video feeds.");

        if (dto.Type != FeedType.Video && !string.IsNullOrEmpty(dto.VideoUrl))
            throw new ArgumentException("Video URL is only allowed for video feeds.");
    }
}
