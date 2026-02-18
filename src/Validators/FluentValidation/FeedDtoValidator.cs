using FeedsWebApi.Dtos;
using FeedsWebApi.Enums;
using FluentValidation;

namespace FeedsWebApi.Validators.FluentValidatiom;

public abstract class FeedDtoValidator<T> : AbstractValidator<T>
    where T : FeedBaseDto
{
    protected FeedDtoValidator()
    {

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Type is invalid.");

        RuleFor(x => x.Image)
            .NotNull()
            .When(x => x.Type == FeedType.Image)
            .WithMessage("Image is required for image feeds.");

        RuleFor(x => x.Image)
            .Null()
            .When(x => x.Type == FeedType.Text)
            .WithMessage("Image is not allowed for text feeds.");

        RuleFor(x => x.VideoUrl)
            .NotEmpty()
            .When(x => x.Type == FeedType.Video)
            .WithMessage("Video URL is required for video feeds.");

        RuleFor(x => x.VideoUrl)
            .Empty()
            .When(x => x.Type != FeedType.Video)
            .WithMessage("Video URL is only allowed for video feeds.");
    }
}

public class FeedCreateDtoValidator : FeedDtoValidator<FeedCreateDto> { }

public class FeedUpdateDtoValidator : FeedDtoValidator<FeedUpdateDto> { }
