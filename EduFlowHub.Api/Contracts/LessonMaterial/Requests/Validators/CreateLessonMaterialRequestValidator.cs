namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests.Validators;

public  class CreateLessonMaterialRequestValidator
    : AbstractValidator<CreateLessonMaterialRequest>
{
    public CreateLessonMaterialRequestValidator()
    {
        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(x => x.OrderNumber)
            .GreaterThan(0);

        RuleFor(x => x.File)
            .Cascade(CascadeMode.Stop)
            .NotNull();
    }
}