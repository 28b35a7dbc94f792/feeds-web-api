using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using FeedsWebApi.Factories;
using FeedsWebApi.Helpers;
using FeedsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApi.Services;

public interface IFeedService
{
    Task<List<FeedResponseDto>> GetAllAsync();
    Task<FeedResponseDto?> GetAsync(int id);
    Task<ImageDto?> GetImageAsync(int id);
    Task<int> CreateAsync(FeedCreateDto dto);
    Task<FeedResponseDto?> UpdateAsync(int id, FeedUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

public class FeedService : IFeedService
{
    private readonly AppDbContext _context;
    private readonly IFeedResponseDtoFactory _feedResponseDtoFactory;
    private readonly IImageHelper _imageHelper;

    public FeedService(
        AppDbContext context,
        IFeedResponseDtoFactory feedResponseDtoFactory,
        IImageHelper imageHelper)
    {
        _context = context;
        _feedResponseDtoFactory = feedResponseDtoFactory;
        _imageHelper = imageHelper;
    }

    public async Task<List<FeedResponseDto>> GetAllAsync()
    {
        var feeds = await _context.Feeds
            .OrderByDescending(f => f.CreatedAt)
            .Include(f => f.Likes)
            .ToListAsync();

        return feeds
            .Select(f => _feedResponseDtoFactory.Create(f))
            .ToList();
    }

    public async Task<FeedResponseDto?> GetAsync(int id)
    {
        var feed = await _context.Feeds
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

    public async Task<int> CreateAsync(FeedCreateDto dto)
    {
        var feed = new Feed
        {
            Title = dto.Title,
            Description = dto.Description,
            PublishingUserId = dto.PublishingUserId,
            Type = dto.Type,
            VideoUrl = dto.VideoUrl
        };

        if (dto.Image != null)
            await _imageHelper.PopulateImageAsync(feed, dto.Image);

        _context.Feeds.Add(feed);

        await _context.SaveChangesAsync();

        return feed.Id;
    }

    public async Task<FeedResponseDto?> UpdateAsync(int id, FeedUpdateDto dto)
    {
        var feed = await _context.Feeds.FindAsync(id);

        if (feed == null)
            return null;

        feed.Title = dto.Title;
        feed.Description = dto.Description;
        feed.Type = dto.Type;
        feed.VideoUrl = dto.VideoUrl;

        if (dto.Image != null)
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
