using FeedsWebApi.Enums;

namespace FeedsWebApi.Dtos;

public abstract class FeedBaseDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public FeedType Type { get; set; }

    public IFormFile? Image { get; set; }
    
    public string? VideoUrl { get; set; }
}
