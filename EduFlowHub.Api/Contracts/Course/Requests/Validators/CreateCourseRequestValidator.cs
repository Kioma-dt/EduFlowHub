namespace EduFlowHub.Api.Contracts.Course.Requests.Validators;

public class CreateCourseRequestValidator
    : AbstractValidator<CreateCourseRequest>
{
    public CreateCourseRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(request => request.Description)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(request => request.Price)
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(10, 2, false);

        RuleFor(request => request.AuthorId)
            .NotEmpty();

        RuleFor(request => request.CategoryId)
            .NotEmpty();
    }
}