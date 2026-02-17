using CodeHollow.FeedReader;
using FeedsWebApi.Dtos;
using FeedsWebApi.Factories;

namespace FeedsWebApi.Helpers;

public interface IRemoteFeedHelper
{
    Task<List<FeedResponseDto>> ReadAsync();
}

public class RemoteFeedHelper : IRemoteFeedHelper
{
    private readonly IConfiguration _config;
    private readonly IFeedResponseDtoFactory _feedResponseDtoFactory;

    public RemoteFeedHelper(
        IConfiguration config,
        IFeedResponseDtoFactory feedResponseDtoFactory)
    {
        _config = config;
        _feedResponseDtoFactory = feedResponseDtoFactory;
    }

    public async Task<List<FeedResponseDto>> ReadAsync()
    {
        var url = _config.GetValue<string>("RemoteFeedSettings:Url");
        int maxItems = _config.GetValue<int>("RemoteFeedSettings:MaxItems");
        
        if (string.IsNullOrEmpty(url) || maxItems < 1)
            return new List<FeedResponseDto>();

        try
        {
            var feed = await FeedReader.ReadAsync(url);

            return feed.Items
                .Take(maxItems)
                .Select(i => _feedResponseDtoFactory.Create(
                    i.Title,
                    i.Description,
                    i.Author,
                    i.PublishingDate
                ))
                .ToList();
        }
        catch
        {
            return new List<FeedResponseDto>();
        }
    }
}
