using FeedsWebApi.Data;
using FeedsWebApi.Models;
using FeedsWebApi.Validators;
using FeedsWebApiTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApiTests.Validators;

[TestFixture]
public class FeedValidatorTests
{
    private FeedValidator _validator;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var appDbContextMock = new AppDbContext(options);

        appDbContextMock.Users.Add(new User
        {
            Id = 2,
            Username = "lisa.nova",
            Name = "Lisa Nova"
        });

        appDbContextMock.SaveChanges();

        _validator = new FeedValidator(appDbContextMock);
    }

    [Test]
    public void Test_That_Validator_Passes_On_Valid_Dto()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Validator_Throws_On_Author_Invalid()
    {
        var dto = ValidatorTestHelper.MockValidCreateTextFeedDto();

        dto.AuthorId = 99;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }
}
