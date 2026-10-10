namespace EduFlowHub.Api.Contracts.User.Requests.Validators;

public class ChangeUserRoleRequestValidator
    : AbstractValidator<ChangeUserRoleRequest>
{
    public  ChangeUserRoleRequestValidator()
    {
        RuleFor(request => request.Role)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Role is undefined");

    }
}