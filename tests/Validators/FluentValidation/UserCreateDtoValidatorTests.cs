using FeedsWebApi.Validators.FluentValidatiom;
using FeedsWebApiTests.Helpers;

namespace UsersWebApiTests.Validators.FluentValidation;

[TestFixture]
public class UserCreateDtoValidatorTests
{
    private UserCreateDtoValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new UserCreateDtoValidator();
    }

    [Test]
    public void Test_That_Validator_Passes_On_Valid_Dto()
    {
        var dto = ValidatorTestHelper.MockValidUserCreateDto();

        var result = _validator.Validate(dto);

        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void Test_That_Validator_Throws_On_Username_Missing()
    {
        var dto = ValidatorTestHelper.MockValidUserCreateDto();

        dto.Username = string.Empty;

        var result = _validator.Validate(dto);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Any(e => e.PropertyName == nameof(dto.Username)));
    }
}
