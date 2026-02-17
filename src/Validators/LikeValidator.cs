using FeedsWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApi.Validators;

public interface ILikeValidator
{
    Task<bool> Validate(int userId, int feedId);
}

public class LikeValidator : ILikeValidator
{
    private readonly AppDbContext _context;

    public LikeValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Validate(int userId, int feedId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            throw new ArgumentException("User ID is invalid.");

        var feed = await _context.Feeds.FindAsync(feedId);

        if (feed == null)
            throw new ArgumentException("Feed ID is invalid.");

        var likeExists = await _context.Likes
            .AnyAsync(l => l.UserId == userId && l.FeedId == feedId);

        return likeExists;
    }
}
