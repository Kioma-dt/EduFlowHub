namespace EduFlowHub.Api.Contracts.CurrentUser.Requests.Validation;

public class ChangeCurrentUserPasswordValidator
    : AbstractValidator<ChangeCurrentUserPasswordRequest>
{
    public ChangeCurrentUserPasswordValidator()
    {
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
        
        RuleFor(request => request.OldPassword)
            .NotEmpty()
            .MaximumLength(128);
    }
}