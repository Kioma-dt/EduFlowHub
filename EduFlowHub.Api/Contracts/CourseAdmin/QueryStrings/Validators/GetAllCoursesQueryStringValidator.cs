namespace EduFlowHub.Api.Contracts.CourseAdmin.QueryStrings.Validators;

public class GetAllCoursesQueryStringValidator
    : AbstractValidator<GetAllCoursesQueryString>
{
    public  GetAllCoursesQueryStringValidator()
    {
        RuleFor(query => query.Category)
            .NotEmpty()
            .MaximumLength(128)
            .When(query => query.Category is not null);
        
        RuleFor(query => query.Status)
            .IsInEnum()
            .When(query => query.Status is not null);
        
        RuleFor(query => query.AuthorId)
            .NotEmpty()
            .When(query => query.AuthorId is not null);
    }
}