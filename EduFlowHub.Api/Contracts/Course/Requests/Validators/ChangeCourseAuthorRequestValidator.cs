namespace EduFlowHub.Api.Contracts.Course.Requests.Validators;

public class ChangeCourseAuthorRequestValidator
    : AbstractValidator<ChangeCourseAuthorRequest>
{
    public ChangeCourseAuthorRequestValidator()
    {
        RuleFor(request => request.AuthorId)
            .NotEmpty();
    }
}