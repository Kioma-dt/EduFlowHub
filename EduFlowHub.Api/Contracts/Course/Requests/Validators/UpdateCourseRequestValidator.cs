namespace EduFlowHub.Api.Contracts.Course.Requests.Validators;

public class UpdateCourseRequestValidator
    : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseRequestValidator()
    {
        RuleFor(x => x.Title)
            .Must(title => title is null ||
                           !string.IsNullOrWhiteSpace(title))
            .WithMessage("Course Name must not be only with whitespaces")
            .MaximumLength(128)
            .When(x => x.Title is not null);

        RuleFor(x => x.Description)
            .Must(description => description is null ||
                                 !string.IsNullOrWhiteSpace(description))
            .WithMessage("Course Description must not be only with whitespaces")
            .MaximumLength(1024)
            .When(x => x.Description is not null);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(10, 2, false)
            .When(x => x.Price.HasValue);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .When(x => x.CategoryId.HasValue);

        RuleFor(x => x)
            .Must(x =>
                x.Title is not null ||
                x.Description is not null ||
                x.Price.HasValue ||
                x.CategoryId.HasValue)
            .WithMessage("At least one category must be specified");
    }
}