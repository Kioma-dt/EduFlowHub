namespace EduFlowHub.Api.Contracts.User.Requests.Validators;

public class UpdateUserRequestValidator
    : AbstractValidator<CreateUserRequest>
{
    public  UpdateUserRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("Firstname cannot contain whitespaces");
        
        RuleFor(request => request.LastName)
            .NotEmpty()
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("Lastname cannot contain whitespaces");
    }
}