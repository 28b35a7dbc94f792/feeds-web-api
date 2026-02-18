using FeedsWebApi.Data;
using FeedsWebApi.Dtos;

namespace FeedsWebApi.Validators;

public interface IFeedValidator
{
    Task ValidateCreate(FeedCreateDto dto);
}

public class FeedValidator : IFeedValidator
{
    private readonly AppDbContext _context;

    public FeedValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task ValidateCreate(FeedCreateDto dto)
    {
        var author = await _context.Users.FindAsync(dto.AuthorId);

        if (author == null)
            throw new ArgumentException("Author ID is invalid.");
    }
}
