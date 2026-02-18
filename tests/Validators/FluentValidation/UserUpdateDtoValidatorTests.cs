using FeedsWebApi.Validators.FluentValidatiom;
using FeedsWebApiTests.Helpers;

namespace UsersWebApiTests.Validators.FluentValidation;

[TestFixture]
public class UserUpdateDtoValidatorTests
{
    private UserUpdateDtoValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new UserUpdateDtoValidator();
    }

    [Test]
    public void Test_That_Validator_Passes_On_Valid_Dto()
    {
        var dto = ValidatorTestHelper.MockValidUserUpdateDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Validator_Throws_On_Username_Missing()
    {
        var dto = ValidatorTestHelper.MockValidUserUpdateDto();

        dto.Username = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Username)));
    }
}
