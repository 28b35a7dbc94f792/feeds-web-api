using FeedsWebApi.Data;
using FeedsWebApi.Dtos;
using FeedsWebApi.Models;
using FeedsWebApi.Validators;
using Microsoft.EntityFrameworkCore;

namespace FeedsWebApiTests;

[TestFixture]
public class UserDtoValidatorTests
{
    private UserDtoValidator _validator;

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

        _validator = new UserDtoValidator(appDbContextMock);
    }

    #region CREATE

    [Test]
    public void Test_That_Create_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidUserCreateDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateCreate(dto));
    }

   [Test]
    public void Test_That_Create_Validator_Throws_On_Null_Dto()
    {
        UserCreateDto dto = null;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Username_Missing()
    {
        var dto = MockValidUserCreateDto();

        dto.Username = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    [Test]
    public void Test_That_Create_Validator_Throws_On_Username_Taken()
    {
        var dto = MockValidUserCreateDto();

        dto.Username = "lisa.nova";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateCreate(dto));
    }

    #endregion

    #region UPDATE

    [Test]
    public void Test_That_Update_Validator_Passes_On_Valid_Dto()
    {
        var dto = MockValidUserUpdateDto();

        Assert.DoesNotThrowAsync(async () => await _validator.ValidateUpdate(3, dto));
    }

   [Test]
    public void Test_That_Update_Validator_Throws_On_Null_Dto()
    {
        UserUpdateDto dto = null;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(3, dto));
    }

    [Test]
    public void Test_That_Update_Validator_Throws_On_Username_Missing()
    {
        var dto = MockValidUserUpdateDto();

        dto.Username = string.Empty;

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(3, dto));
    }

    [Test]
    public void Test_That_Update_Validator_Throws_On_Username_Taken()
    {
        var dto = MockValidUserUpdateDto();

        dto.Username = "lisa.nova";

        Assert.ThrowsAsync<ArgumentException>(async () => await _validator.ValidateUpdate(3, dto));
    }

    #endregion

    #region HELPER METHODS

    private UserCreateDto MockValidUserCreateDto()
    {
        return new UserCreateDto
        {
            Username = "Cypress Vale",
            Name = "cypress.vale"
        };
    }

    private UserUpdateDto MockValidUserUpdateDto()
    {
        return new UserUpdateDto
        {
            Username = "Cypress V.",
            Name = "cypress.v"
        };
    }

    #endregion
}
