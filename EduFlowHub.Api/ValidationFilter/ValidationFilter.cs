using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EduFlowHub.Api.ValidationFilter;

public class ValidationFilter
    : IAsyncActionFilter
{
    private readonly IServiceProvider _services;

    public ValidationFilter(IServiceProvider services)
    {
        _services = services;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var failures = new List<ValidationFailure>();
        
        foreach (var arg in context.ActionArguments.Values.OfType<object>())
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
            if (_services.GetService(validatorType) is not IValidator validator)
                continue;

            var result = await validator.ValidateAsync(
                new ValidationContext<object>(arg),
                context.HttpContext.RequestAborted);

            failures.AddRange(result.Errors);
        }
        
        if (failures.Count > 0)
            throw new ValidationException(failures);

        await next();
    }
}