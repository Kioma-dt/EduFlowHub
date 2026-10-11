namespace EduFlowHub.Api.Contracts.CourseAdmin.Requests.Validators;

public class ChangeCourseAuthorRequestValidator
    : AbstractValidator<ChangeCourseAuthorRequest>
{
    public ChangeCourseAuthorRequestValidator()
    {
        RuleFor(request => request.AuthorId)
            .NotEmpty();
    }
}