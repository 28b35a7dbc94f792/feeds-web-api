using FeedsWebApi.Enums;
using FeedsWebApi.Validators.FluentValidatiom;
using FeedsWebApiTests.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FeedsWebApiTests.Validators.FluentValidation;

[TestFixture]
public class FeedCreateDtoValidatorTests
{
    private FeedCreateDtoValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new FeedCreateDtoValidator();
    }

    [Test]
    public void Test_That_Text_Feed_Validator_Passes_On_Valid_Dto()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Image_Feed_Validator_Passes_On_Valid_Dto()
    {
        var dto =  ValidatorTestHelper.MockValidCreateImageFeedDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Video_Feed_Validator_Passes_On_Valid_Dto()
    {
        var dto =  ValidatorTestHelper.MockValidCreateVideoFeedDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Validator_Throws_On_Title_Missing()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        dto.Title = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Title)));
    }

    [Test]
    public void Test_That_Validator_Throws_On_Description_Missing()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        dto.Description = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Description)));
    }

    [Test]
    public void Test_That_Validator_Throws_On_Feed_Type_Invalid()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        dto.Type = (FeedType)13;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Type)));
    }

    [Test]
    public void Test_That_Text_Feed_Validator_Throws_On_Image_Present()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        dto.Image = new Mock<IFormFile>().Object;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Image)));
    }

    [Test]
    public void Test_That_Text_Feed_Validator_Throws_On_Video_Url_Present()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        dto.VideoUrl = "http://xzy";

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.VideoUrl)));
    }

    [Test]
    public void Test_That_Image_Feed_Validator_Throws_On_Image_Missing()
    {
        var dto = ValidatorTestHelper.MockValidCreateImageFeedDto();

        dto.Image = null;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Image)));
    }

    [Test]
    public void Test_That_Image_Feed_Validator_Throws_On_Video_Url_Present()
    {
        var dto = ValidatorTestHelper.MockValidCreateImageFeedDto();

        dto.VideoUrl = "http://xzy";

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.VideoUrl)));
    }

    [Test]
    public void Test_That_Video_Feed_Validator_Throws_On_Video_Url_Missing()
    {
        var dto = ValidatorTestHelper.MockValidCreateVideoFeedDto();

        dto.VideoUrl = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.VideoUrl)));
    }
}
