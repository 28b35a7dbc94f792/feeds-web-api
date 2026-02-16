namespace FeedsWebApi.Dtos;

public class FeedCreateDto : FeedBaseDto
{
    public int PublishingUserId { get; set; }

    public IFormFile? Image { get; set; }
}
