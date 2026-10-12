namespace EduFlowHub.Api.Contracts.User.Requests.Validators;

public class UpdateUserRequestValidator
    : AbstractValidator<UpdateUserRequest>
{
    public  UpdateUserRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("Firstname cannot contain whitespaces")
            .When(request => request?.FirstName is not null);
        
        RuleFor(request => request.LastName)
            .NotEmpty()
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("Lastname cannot contain whitespaces")
            .When(request => request?.LastName is not null);
        
        RuleFor(request => request)
            .Must(request =>
                request?.FirstName is not null 
                || request?.LastName is not null) 
            .WithMessage("At least one parameter must be specified");
    }
}