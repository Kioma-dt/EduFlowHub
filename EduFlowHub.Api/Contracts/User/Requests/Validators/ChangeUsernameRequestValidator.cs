namespace EduFlowHub.Api.Contracts.User.Requests.Validators;

public class ChangeUsernameRequestValidator
    : AbstractValidator<ChangeUsernameRequest>
{
    public  ChangeUsernameRequestValidator()
    {
        RuleFor(request => request.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(128)
            .Must(username => !username.Any(char.IsWhiteSpace))
            .WithMessage("Username cannot contain whitespaces");

    }
}