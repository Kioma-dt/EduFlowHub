namespace EduFlowHub.Api.Contracts.CourseCategory.Requests.Validators;

public class PutCourseCategoryRequestValidator
    : AbstractValidator<PutCourseCategoryRequest>
{
    public PutCourseCategoryRequestValidator()
    {
        RuleFor(courseCategory => courseCategory.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(128);
    }
}