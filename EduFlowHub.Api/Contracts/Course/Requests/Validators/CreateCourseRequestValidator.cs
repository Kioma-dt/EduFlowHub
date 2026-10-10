namespace EduFlowHub.Api.Contracts.Course.Requests.Validators;

public class CreateCourseRequestValidator
    : AbstractValidator<CreateCourseRequest>
{
    public CreateCourseRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .PrecisionScale(10, 2, false);

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}