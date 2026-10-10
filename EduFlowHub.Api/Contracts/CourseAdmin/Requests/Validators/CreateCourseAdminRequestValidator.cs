namespace EduFlowHub.Api.Contracts.CourseAdmin.Requests.Validators;

public class CreateCourseAdminRequestValidator
    : AbstractValidator<CreateCourseAdminRequest>
{
    public CreateCourseAdminRequestValidator()
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

        RuleFor(x => x.AuthorId)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}