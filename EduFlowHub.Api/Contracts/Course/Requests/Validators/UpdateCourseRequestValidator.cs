namespace EduFlowHub.Api.Contracts.Course.Requests.Validators;

public class UpdateCourseRequestValidator
    : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseRequestValidator()
    {
        RuleFor(request => request.Title)
            .Must(title => title is null ||
                           !string.IsNullOrWhiteSpace(title))
            .WithMessage("Course Name must not be only with whitespaces")
            .MaximumLength(128)
            .When(request => request.Title is not null);

        RuleFor(request => request.Description)
            .Must(description => description is null ||
                                 !string.IsNullOrWhiteSpace(description))
            .WithMessage("Course Description must not be only with whitespaces")
            .MaximumLength(1024)
            .When(request => request.Description is not null);

        RuleFor(request => request.Price)
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(10, 2, false)
            .When(request => request.Price.HasValue);

        RuleFor(request => request.CategoryId)
            .NotEmpty()
            .When(request => request.CategoryId.HasValue);

        RuleFor(request => request)
            .Must(request =>
                request.Title is not null ||
                request.Description is not null ||
                request.Price.HasValue ||
                request.CategoryId.HasValue)
            .WithMessage("At least one category must be specified");
    }
}