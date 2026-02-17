using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using FeedsWebApi.Enums;
using FeedsWebApi.Models;
using FeedsWebApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FeedsWebApiTests;

[TestFixture]
public class FeedDtoValidatorTests
{
    private FeedDtoValidator _validator;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("dbMock")
            .Options;

        var appDbContextMock = new AppDbContext(options);

        appDbContextMock.Users.Add(new User
        {
            Id = 2,
            Username = "lisa.nova",
            Name = "Lisa Nova"
        });

        appDbContextMock.SaveChanges();

        _validator = new FeedDtoValidator(appDbContextMock);
    }

    #region CREATE

    [Test]
    public void Test_That_Text_Feed_Create_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidCreateTextFeedDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Image_Feed_Create_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidCreateImageFeedDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Video_Feed_Create_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidCreateVideoFeedDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Null_Dto()
    {
        FeedCreateDto dto = null;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Title_Missing()
    {
        var dto = MockValidCreateTextFeedDto();

        dto.Title = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Description_Missing()
    {
        var dto = MockValidCreateTextFeedDto();

        dto.Description = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Author_Invalid()
    {
        var dto = MockValidCreateTextFeedDto();

        dto.AuthorId = 99;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Feed_Type_Invalid()
    {
        var dto = MockValidCreateTextFeedDto();

        dto.Type = (FeedType)13;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Text_Feed_Create_Validator_Throws_On_Image_Present()
    {
        var dto = MockValidCreateTextFeedDto();

        dto.Image = new Mock<IFormFile>().Object;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Text_Feed_Create_Validator_Throws_On_Video_Url_Present()
    {
        var dto = MockValidCreateTextFeedDto();

        dto.VideoUrl = "http://xzy";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Image_Feed_Create_Validator_Throws_On_Image_Missing()
    {
        var dto = MockValidCreateImageFeedDto();

        dto.Image = null;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Image_Feed_Create_Validator_Throws_On_Video_Url_Present()
    {
        var dto = MockValidCreateImageFeedDto();

        dto.VideoUrl = "http://xzy";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Video_Feed_Create_Validator_Throws_On_Video_Url_Missing()
    {
        var dto = MockValidCreateVideoFeedDto();

        dto.VideoUrl = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    #endregion

    #region UPDATE

    [Test]
    public void Test_That_Text_Feed_Update_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidUpdateTextFeedDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Image_Feed_Update_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidUpdateImageFeedDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Video_Feed_Update_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidUpdateVideoFeedDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Update_Validator_Throws_On_Null_Dto()
    {
        FeedUpdateDto dto = null;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Update_Validator_Throws_On_Title_Missing()
    {
        var dto = MockValidUpdateTextFeedDto();

        dto.Title = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Update_Validator_Throws_On_Description_Missing()
    {
        var dto = MockValidUpdateTextFeedDto();

        dto.Description = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Update_Validator_Throws_On_Feed_Type_Invalid()
    {
        var dto = MockValidUpdateTextFeedDto();

        dto.Type = (FeedType)13;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Text_Feed_Update_Validator_Throws_On_Image_Present()
    {
        var dto = MockValidUpdateTextFeedDto();

        dto.Image = new Mock<IFormFile>().Object;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Text_Feed_Update_Validator_Throws_On_Video_Url_Present()
    {
        var dto = MockValidUpdateTextFeedDto();

        dto.VideoUrl = "http://xzy";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Image_Feed_Update_Validator_Throws_On_Image_Missing()
    {
        var dto = MockValidUpdateImageFeedDto();

        dto.Image = null;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Image_Feed_Update_Validator_Throws_On_Video_Url_Present()
    {
        var dto = MockValidUpdateImageFeedDto();

        dto.VideoUrl = "http://xzy";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    [Test]
    public void Test_That_Video_Feed_Update_Validator_Throws_On_Video_Url_Missing()
    {
        var dto = MockValidUpdateVideoFeedDto();

        dto.VideoUrl = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(dto));
    }

    #endregion

    #region HELPER METHODS

    private FeedCreateDto MockValidCreateTextFeedDto()
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

    private FeedCreateDto MockValidCreateImageFeedDto()
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

    private FeedCreateDto MockValidCreateVideoFeedDto()
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

    private FeedUpdateDto MockValidUpdateTextFeedDto()
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

    private FeedUpdateDto MockValidUpdateImageFeedDto()
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

    private FeedUpdateDto MockValidUpdateVideoFeedDto()
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

    #endregion
}
