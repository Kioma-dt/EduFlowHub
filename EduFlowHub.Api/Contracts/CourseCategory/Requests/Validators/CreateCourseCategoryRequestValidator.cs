namespace EduFlowHub.Api.Contracts.CourseCategory.Requests.Validators;

public class CreateCourseCategoryRequestValidator
    : AbstractValidator<CreateCourseCategoryRequest>
{
    public CreateCourseCategoryRequestValidator()
    {
        RuleFor(courseCategory => courseCategory.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(128);
    }
}