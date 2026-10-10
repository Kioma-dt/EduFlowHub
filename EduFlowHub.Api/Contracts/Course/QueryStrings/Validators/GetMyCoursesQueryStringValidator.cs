namespace EduFlowHub.Api.Contracts.Course.QueryStrings.Validators;

public class GetMyCoursesQueryStringValidator
    : AbstractValidator<GetMyCoursesQueryString>
{
    public  GetMyCoursesQueryStringValidator()
    {
        RuleFor(query => query.Category)
            .NotEmpty()
            .MaximumLength(128)
            .When(query => query.Category is not null);
        
        RuleFor(query => query.Status)
            .IsInEnum()
            .When(query => query.Status is not null);
    }
}