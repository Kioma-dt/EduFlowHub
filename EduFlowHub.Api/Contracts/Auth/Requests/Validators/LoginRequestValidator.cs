namespace EduFlowHub.Api.Contracts.Auth.Requests.Validators;

public class LoginRequestValidator
    : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Username)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(request => request.Password)
            .NotEmpty()
            .MaximumLength(128);
    }
}