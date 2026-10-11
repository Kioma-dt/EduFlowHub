namespace EduFlowHub.Api.Contracts.CourseCategory.Requests.Validators;

public class PutCourseCategoryRequestValidator
    : AbstractValidator<PutCourseCategoryRequest>
{
    public PutCourseCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(128);
    }
}