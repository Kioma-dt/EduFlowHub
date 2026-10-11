namespace EduFlowHub.Api.Contracts.Course.QueryStrings.Validators;

public class GetAllCoursesQueryStringValidator
    : AbstractValidator<GetAllCoursesQueryString>
{
    public  GetAllCoursesQueryStringValidator()
    {
        RuleFor(query => query.CategoryNormalizedName)
            .NotEmpty()
            .MaximumLength(128)
            .When(query => query.CategoryNormalizedName is not null);
        
        RuleFor(query => query.Status)
            .IsInEnum()
            .When(query => query.Status is not null);
        
        RuleFor(query => query.AuthorId)
            .NotEmpty()
            .When(query => query.AuthorId is not null);
    }
}