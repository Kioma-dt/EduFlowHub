namespace EduFlowHub.Api.Contracts.User.Requests.Validators;

public class CreateUserRequestValidator
    : AbstractValidator<CreateUserRequest>
{
    public  CreateUserRequestValidator()
    {
        RuleFor(request => request.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("Username cannot contain whitespaces");

        RuleFor(request => request.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128)
            .Matches(@"[A-Z]")
            .WithMessage("Password must contain uppercase letter")
            .Matches(@"[a-z]")
            .WithMessage("Password must contain lowercase letter.")
            .Matches(@"[0-9]")
            .WithMessage("Password must contain numeric character")
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("Password cannot contain whitespaces");

        RuleFor(request => request.Role)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Role is undefined");
        
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("FirstName cannot contain whitespaces");
        
        RuleFor(request => request.LastName)
            .NotEmpty()
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("LastName cannot contain whitespaces");
    }
}