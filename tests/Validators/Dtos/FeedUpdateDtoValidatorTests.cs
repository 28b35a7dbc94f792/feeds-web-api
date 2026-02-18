using FeedsWebApi.Enums;
using FeedsWebApi.Validators.Dtos;
using FeedsWebApiTests.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FeedsWebApiTests.Validators.Dtos;

[TestFixture]
public class FeedUpdateDtoValidatorTests
{
    private FeedUpdateDtoValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new FeedUpdateDtoValidator();
    }

    [Test]
    public void Test_That_Text_Feed_Validator_Passes_On_Valid_Dto()
    {
        var dto = ValidatorTestHelper.MockValidUpdateTextFeedDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Image_Feed_Validator_Passes_On_Valid_Dto()
    {
        var dto =  ValidatorTestHelper.MockValidUpdateImageFeedDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Video_Feed_Validator_Passes_On_Valid_Dto()
    {
        var dto =  ValidatorTestHelper.MockValidUpdateVideoFeedDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Validator_Throws_On_Title_Missing()
    {
        var dto = ValidatorTestHelper.MockValidUpdateTextFeedDto();

        dto.Title = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Title)));
    }

    [Test]
    public void Test_That_Validator_Throws_On_Description_Missing()
    {
        var dto = ValidatorTestHelper.MockValidUpdateTextFeedDto();

        dto.Description = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Description)));
    }

    [Test]
    public void Test_That_Validator_Throws_On_Feed_Type_Invalid()
    {
        var dto = ValidatorTestHelper.MockValidUpdateTextFeedDto();

        dto.Type = (FeedType)13;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Type)));
    }

    [Test]
    public void Test_That_Text_Feed_Validator_Throws_On_Image_Present()
    {
        var dto = ValidatorTestHelper.MockValidUpdateTextFeedDto();

        dto.Image = new Mock<IFormFile>().Object;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Image)));
    }

    [Test]
    public void Test_That_Text_Feed_Validator_Throws_On_Video_Url_Present()
    {
        var dto = ValidatorTestHelper.MockValidUpdateTextFeedDto();

        dto.VideoUrl = "http://xzy";

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.VideoUrl)));
    }

    [Test]
    public void Test_That_Image_Feed_Validator_Throws_On_Image_Missing()
    {
        var dto = ValidatorTestHelper.MockValidUpdateImageFeedDto();

        dto.Image = null;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Image)));
    }

    [Test]
    public void Test_That_Image_Feed_Validator_Throws_On_Video_Url_Present()
    {
        var dto = ValidatorTestHelper.MockValidUpdateImageFeedDto();

        dto.VideoUrl = "http://xzy";

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.VideoUrl)));
    }

    [Test]
    public void Test_That_Video_Feed_Validator_Throws_On_Video_Url_Missing()
    {
        var dto = ValidatorTestHelper.MockValidUpdateVideoFeedDto();

        dto.VideoUrl = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.VideoUrl)));
    }
}
