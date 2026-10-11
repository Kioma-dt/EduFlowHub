namespace EduFlowHub.Api.Contracts.Course.QueryStrings.Validators;

public class GetStudentEnrolledCoursesStringValidator
    : AbstractValidator<GetStudentEnrolledCoursesQueryString>
{
    public  GetStudentEnrolledCoursesStringValidator()
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