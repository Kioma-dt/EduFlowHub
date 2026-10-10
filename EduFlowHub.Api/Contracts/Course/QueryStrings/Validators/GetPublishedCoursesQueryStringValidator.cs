namespace EduFlowHub.Api.Contracts.Course.QueryStrings.Validators;

public class GetPublishedCoursesQueryStringValidator
    : AbstractValidator<GetPublishedCoursesQueryString>
{
    public  GetPublishedCoursesQueryStringValidator()
    {
        RuleFor(query => query.Category)
            .NotEmpty()
            .MaximumLength(128)
            .When(query => query.Category is not null);
        
        RuleFor(query => query.AuthorId)
            .NotEmpty()
            .When(query => query.AuthorId is not null);
    }
}