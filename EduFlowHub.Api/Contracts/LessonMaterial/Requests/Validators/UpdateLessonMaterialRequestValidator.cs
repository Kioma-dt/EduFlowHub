namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests.Validators;

public class UpdateLessonMaterialRequestValidator
    : AbstractValidator<UpdateLessonMaterialRequest>
{
    public UpdateLessonMaterialRequestValidator()
    {
        RuleFor(x => x.Title)
            .Must(title =>
                title is null || !string.IsNullOrWhiteSpace(title))
            .WithMessage("Lesson Material title cannot be only whitespaces")
            .MaximumLength(128)
            .When(x => x.Title is not null);

        RuleFor(x => x.OrderNumber)
            .GreaterThan(0)
            .When(x => x.OrderNumber.HasValue);

        RuleFor(x => x)
            .Must(x => x.Title is not null || x.OrderNumber.HasValue)
            .WithMessage("At least one parameter must be specified");
    }
}