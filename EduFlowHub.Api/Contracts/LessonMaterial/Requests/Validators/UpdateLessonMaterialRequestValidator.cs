namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests.Validators;

public class UpdateLessonMaterialRequestValidator
    : AbstractValidator<UpdateLessonMaterialRequest>
{
    public UpdateLessonMaterialRequestValidator()
    {
        RuleFor(request => request.Title)
            .Must(title =>
                title is null || !string.IsNullOrWhiteSpace(title))
            .WithMessage("Lesson Material title cannot be only whitespaces")
            .MaximumLength(128)
            .When(x => x.Title is not null);

        RuleFor(request => request.OrderNumber)
            .GreaterThan(0)
            .When(request => request.OrderNumber.HasValue);

        RuleFor(request => request)
            .Must(request => request.Title is not null || request.OrderNumber.HasValue)
            .WithMessage("At least one parameter must be specified");
    }
}