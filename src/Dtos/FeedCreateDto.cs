namespace FeedsWebApi.Dtos;

public class FeedCreateDto : FeedBaseDto
{
    public int AuthorId { get; set; }

    public IFormFile? Image { get; set; }
}
