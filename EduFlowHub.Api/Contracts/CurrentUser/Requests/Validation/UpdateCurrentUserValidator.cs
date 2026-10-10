namespace EduFlowHub.Api.Contracts.CurrentUser.Requests.Validation;

public class UpdateCurrentUserValidator
    : AbstractValidator<UpdateCurrentUserRequest>
{
    public UpdateCurrentUserValidator()
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