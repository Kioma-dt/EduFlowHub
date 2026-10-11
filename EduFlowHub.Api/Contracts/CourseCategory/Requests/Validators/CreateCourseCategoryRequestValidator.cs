namespace EduFlowHub.Api.Contracts.CourseCategory.Requests.Validators;

public class CreateCourseCategoryRequestValidator
    : AbstractValidator<CreateCourseCategoryRequest>
{
    public CreateCourseCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(128);
    }
}