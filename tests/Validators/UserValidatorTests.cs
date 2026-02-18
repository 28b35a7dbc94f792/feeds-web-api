using FeedsWebApi.Data;
using FeedsWebApi.Models;
using FeedsWebApi.Validators;
using FeedsWebApiTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApiTests.Validators;

[TestFixture]
public class UserValidatorTests
{
    private UserValidator _validator;

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

        _validator = new UserValidator(appDbContextMock);
    }

    [Test]
    public void Test_That_Create_Validator_Passes_On_Valid_Dto()
    {
        var dto = ValidatorTestHelper.MockValidUserCreateDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Username_Taken()
    {
        var dto = ValidatorTestHelper.MockValidUserCreateDto();

        dto.Username = "lisa.nova";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Update_Validator_Passes_On_Valid_Dto()
    {
        var dto = ValidatorTestHelper.MockValidUserUpdateDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateUpdate(3, dto));
    }

    [Test]
    public void Test_That_Update_Validator_Throws_On_Username_Taken()
    {
        var dto = ValidatorTestHelper.MockValidUserUpdateDto();

        dto.Username = "lisa.nova";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(3, dto));
    }
}
