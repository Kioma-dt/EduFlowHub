namespace EduFlowHub.Api.Contracts.CourseAdmin.Requests.Validators;

public class CreateCourseAdminRequestValidator
    : AbstractValidator<CreateCourseAdminRequest>
{
    public CreateCourseAdminRequestValidator()
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