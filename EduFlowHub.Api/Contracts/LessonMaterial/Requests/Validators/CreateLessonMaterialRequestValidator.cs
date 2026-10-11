namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests.Validators;

public  class CreateLessonMaterialRequestValidator
    : AbstractValidator<CreateLessonMaterialRequest>
{
    public CreateLessonMaterialRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(request => request.OrderNumber)
            .GreaterThan(0);

        RuleFor(request => request.File)
            .Cascade(CascadeMode.Stop)
            .NotNull();
    }
}