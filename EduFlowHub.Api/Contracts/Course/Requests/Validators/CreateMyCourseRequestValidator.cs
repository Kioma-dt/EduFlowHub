namespace EduFlowHub.Api.Contracts.Course.Requests.Validators;

public class CreateMyCourseRequestValidator
    : AbstractValidator<CreateMyCourseRequest>
{
    public CreateMyCourseRequestValidator()
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

        RuleFor(request => request.CategoryId)
            .NotEmpty();
    }
}