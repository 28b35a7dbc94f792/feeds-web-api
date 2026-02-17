using FeedsWebApi.Models;

namespace FeedsWebApi.Helpers;

public interface IImageHelper
{
    Task PopulateImageAsync(Feed feed, IFormFile image);
}

public class ImageHelper : IImageHelper
{
    public async Task PopulateImageAsync(Feed feed, IFormFile image)
    {
        if (image == null)
        {
            feed.ImageData = null;
            feed.ImageContentType = null;
        }
        else
        {
            using var ms = new MemoryStream();

            await image.CopyToAsync(ms);

            feed.ImageData = ms.ToArray();
            feed.ImageContentType = image.ContentType;
        }
    }
}
