namespace FeedsWebApi.Dtos;

public class FeedUpdateDto : FeedBaseDto
{
    public IFormFile? Image { get; set; }
}
