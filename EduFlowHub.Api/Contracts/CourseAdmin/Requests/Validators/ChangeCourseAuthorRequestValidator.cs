namespace EduFlowHub.Api.Contracts.CourseAdmin.Requests.Validators;

public class ChangeCourseAuthorRequestValidator
    : AbstractValidator<ChangeCourseAuthorRequest>
{
    public ChangeCourseAuthorRequestValidator()
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty();
    }
}