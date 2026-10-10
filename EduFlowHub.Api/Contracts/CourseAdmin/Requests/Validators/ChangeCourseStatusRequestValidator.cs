namespace EduFlowHub.Api.Contracts.CourseAdmin.Requests.Validators;

public class ChangeCourseStatusRequestValidator
    : AbstractValidator<ChangeCourseStatusRequest>
{
    public ChangeCourseStatusRequestValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}