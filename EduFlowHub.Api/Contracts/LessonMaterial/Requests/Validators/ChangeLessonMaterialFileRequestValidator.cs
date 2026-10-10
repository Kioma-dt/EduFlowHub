namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests.Validators;

public class ChangeLessonMaterialFileRequestValidator
    : AbstractValidator<ChangeLessonMaterialFileRequest>
{
    public ChangeLessonMaterialFileRequestValidator()
    {
        RuleFor(x => x.File)
            .Cascade(CascadeMode.Stop)
            .NotNull();
    }
}