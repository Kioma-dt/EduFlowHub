namespace EduFlowHub.Api.Contracts.Course.QueryStrings.Validators;

public class GetPublishedCoursesQueryStringValidator
    : AbstractValidator<GetPublishedCoursesQueryString>
{
    public  GetPublishedCoursesQueryStringValidator()
    {
        RuleFor(query => query.CategoryNormalizedName)
            .NotEmpty()
            .MaximumLength(128)
            .When(query => query.CategoryNormalizedName is not null);
        
        RuleFor(query => query.AuthorId)
            .NotEmpty()
            .When(query => query.AuthorId is not null);
    }
}