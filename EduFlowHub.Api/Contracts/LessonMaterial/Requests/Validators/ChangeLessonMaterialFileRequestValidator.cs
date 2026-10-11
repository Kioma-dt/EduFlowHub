namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests.Validators;

public class ChangeLessonMaterialFileRequestValidator
    : AbstractValidator<ChangeLessonMaterialFileRequest>
{
    public ChangeLessonMaterialFileRequestValidator()
    {
        RuleFor(request => request.File)
            .NotNull();
    }
}