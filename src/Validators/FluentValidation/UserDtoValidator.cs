using FeedsWebApi.Dtos;
using FluentValidation;

namespace FeedsWebApi.Validators.FluentValidatiom;

public abstract class UserDtoValidator<T> : AbstractValidator<T>
    where T : UserBaseDto
{
    protected UserDtoValidator()
    {

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.");
    }
}

public class UserCreateDtoValidator : UserDtoValidator<UserCreateDto> { }

public class UserUpdateDtoValidator : UserDtoValidator<UserUpdateDto> { }
