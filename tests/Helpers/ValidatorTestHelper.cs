using FeedsWebApi.Dtos;
using FeedsWebApi.Enums;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FeedsWebApiTests.Helpers;

public static class ValidatorTestHelper
{
    public static UserCreateDto MockValidUserCreateDto()
    {
        return new UserCreateDto
        {
            Username = "cypress.vale",
            Name = "Cypress Vale"
        };
    }

    public static UserUpdateDto MockValidUserUpdateDto()
    {
        return new UserUpdateDto
        {
            Username = "cypress.v",
            Name = "Cypress V."
        };
    }
    public static FeedCreateDto MockValidCreateTextFeedDto()
    {
        return new FeedCreateDto
        {
            Title = "Snow!!",
            Description = "It's snowing even more!!!",
            AuthorId = 2,
            Type = FeedType.Text,
            Image = null,
            VideoUrl = null
        };
    }

    public static FeedCreateDto MockValidCreateImageFeedDto()
    {
        return new FeedCreateDto
        {
            Title = "Snow!!",
            Description = "It's snowing even more!!!",
            AuthorId = 2,
            Type = FeedType.Image,
            Image = new Mock<IFormFile>().Object,
            VideoUrl = null
        };
    }

    public static FeedCreateDto MockValidCreateVideoFeedDto()
    {
        return new FeedCreateDto
        {
            Title = "Snow!!",
            Description = "It's snowing even more!!!",
            AuthorId = 2,
            Type = FeedType.Video,
            Image = new Mock<IFormFile>().Object,
            VideoUrl = "http://xzy"
        };
    }

    public static FeedUpdateDto MockValidUpdateTextFeedDto()
    {
        return new FeedUpdateDto
        {
            Title = "Snow!!",
            Description = "It's snowing even more!!!",
            Type = FeedType.Text,
            Image = null,
            VideoUrl = null
        };
    }

    public static FeedUpdateDto MockValidUpdateImageFeedDto()
    {
        return new FeedUpdateDto
        {
            Title = "Snow!!",
            Description = "It's snowing even more!!!",
            Type = FeedType.Image,
            Image = new Mock<IFormFile>().Object,
            VideoUrl = null
        };
    }

    public static FeedUpdateDto MockValidUpdateVideoFeedDto()
    {
        return new FeedUpdateDto
        {
            Title = "Snow!!",
            Description = "It's snowing even more!!!",
            Type = FeedType.Video,
            Image = new Mock<IFormFile>().Object,
            VideoUrl = "http://xzy"
        };
    }
}
