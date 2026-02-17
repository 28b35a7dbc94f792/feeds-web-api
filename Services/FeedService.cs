using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using FeedsWebApi.Factories;
using FeedsWebApi.Helpers;
using FeedsWebApi.Validators;
using Microsoft.EntityFrameworkCore;
using Feed = FeedsWebApi.Models.Feed;

namespace FeedsWebApi.Services;

public interface IFeedService
{
    Task<List<FeedResponseDto>> GetAllAsync();
    Task<FeedResponseDto?> GetAsync(int id);
    Task<ImageDto?> GetImageAsync(int id);
    Task<FeedResponseDto?> CreateAsync(FeedCreateDto dto);
    Task<FeedResponseDto?> UpdateAsync(int id, FeedUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

public class FeedService : IFeedService
{
    private readonly AppDbContext _context;
    private readonly IFeedResponseDtoFactory _feedResponseDtoFactory;
    private readonly IFeedDtoValidator _feedDtoValidator;
    private readonly IImageHelper _imageHelper;
    private readonly IRemoteFeedHelper _remoteFeedHelper;

    public FeedService(
        AppDbContext context,
        IFeedResponseDtoFactory feedResponseDtoFactory,
        IFeedDtoValidator feedDtoValidator,
        IImageHelper imageHelper,
        IRemoteFeedHelper remoteFeedHelper)
    {
        _context = context;
        _feedResponseDtoFactory = feedResponseDtoFactory;
        _feedDtoValidator = feedDtoValidator;
        _imageHelper = imageHelper;
        _remoteFeedHelper = remoteFeedHelper;
    }

    public async Task<List<FeedResponseDto>> GetAllAsync()
    {
        var feeds = await _context.Feeds
            .Include(f => f.Author)
            .Include(f => f.Likes)
            .OrderByDescending(f => f.PublishingDate)
            .ToListAsync();

        var feedDtos = feeds
            .Select(f => _feedResponseDtoFactory.Create(f))
            .ToList();

        var remoteFeedDtos = await _remoteFeedHelper.ReadAsync();

        if (remoteFeedDtos.Count != 0)
        {
            feedDtos.AddRange(remoteFeedDtos);

            feedDtos = feedDtos
                .OrderByDescending(f => f.PublishingDate)
                .ToList();
        }

        return feedDtos;
    }

    public async Task<FeedResponseDto?> GetAsync(int id)
    {
        var feed = await _context.Feeds
            .Include(f => f.Author)
            .Include(f => f.Likes)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (feed == null)
            return null;

        return _feedResponseDtoFactory.Create(feed);
    }

    public async Task<ImageDto?> GetImageAsync(int id)
    {
        var feed = await _context.Feeds.FindAsync(id);

        if (feed == null || feed.ImageData == null)
            return null;

        return new ImageDto
        {
            Data = feed.ImageData,
            ContentType = feed.ImageContentType ?? "application/octet-stream"
        };
    }

    public async Task<FeedResponseDto?> CreateAsync(FeedCreateDto dto)
    {
        await _feedDtoValidator.ValidateCreate(dto);

        var feed = new Feed
        {
            Title = dto.Title,
            Description = dto.Description,
            AuthorId = dto.AuthorId,
            Type = dto.Type,
            VideoUrl = dto.VideoUrl
        };

        await _imageHelper.PopulateImageAsync(feed, dto.Image);

        _context.Feeds.Add(feed);

        await _context.SaveChangesAsync();

        return _feedResponseDtoFactory.Create(feed);
    }

    public async Task<FeedResponseDto?> UpdateAsync(int id, FeedUpdateDto dto)
    {
        var feed = await _context.Feeds.FindAsync(id);

        if (feed == null)
            return null;

        await _feedDtoValidator.ValidateUpdate(dto);

        feed.Title = dto.Title;
        feed.Description = dto.Description;
        feed.Type = dto.Type;
        feed.VideoUrl = dto.VideoUrl;

        await _imageHelper.PopulateImageAsync(feed, dto.Image);

        await _context.SaveChangesAsync();

        return await GetAsync(feed.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var feed = await _context.Feeds.FindAsync(id);

        if (feed == null)
            return false;

        _context.Feeds.Remove(feed);

        await _context.SaveChangesAsync();

        return true;
    }
}
