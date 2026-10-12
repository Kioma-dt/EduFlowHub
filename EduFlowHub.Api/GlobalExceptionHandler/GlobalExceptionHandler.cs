using System.Diagnostics;
using EduFlowHub.Application.Exceptions.AccessDeniedExceptions;
using EduFlowHub.Application.Exceptions.ApplicationStateExceptions;
using EduFlowHub.Application.Exceptions.ApplicationValidationExceptions;
using EduFlowHub.Application.Exceptions.ContentTooLarge;
using EduFlowHub.Application.Exceptions.NotFoundExceptions;
using EduFlowHub.Application.Exceptions.TooManyRequestsExceptions;
using EduFlowHub.Application.Exceptions.UnauthorizedExceptions;
using EduFlowHub.Application.Exceptions.UnsupportedFileTypeExceptions;
using EduFlowHub.Domain.Exceptions.DomainStateException;
using EduFlowHub.Domain.Exceptions.DomainValidationExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.GlobalExceptionHandler;

public class GlobalExceptionHandler
    : IExceptionHandler
{
    private readonly IProblemDetailsService  _problemDetailsService;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }
    
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var (status, title) = Map(exception);
        
        httpContext.Response.StatusCode = status;

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = status >= 500 ? null : exception.Message
        };
        
        if (exception is UserIsBlockedExceptiom blocked)
            httpContext.Response.Headers.RetryAfter = ((int)Math.Ceiling(blocked.TryAfter.TotalSeconds)).ToString();
        
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors.GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            problem = new ValidationProblemDetails(errors) { Status = 400, Title = "Validation failed" };
        }

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problem,
            Exception = exception
        });
    }
    
    private static (int Status, string Title) Map(Exception ex) => ex switch
    {
        NotFoundException => (404, "Not Found"),
        AccessDeniedException => (403, "Forbidden"),
        UnauthorizedUserException => (401, "Unauthorized"),
        UnauthorizedAccessException => (401, "Unauthorized"),
        UnauthorizedException => (401, "Unauthorized"),
        ValidationException => (400, "Validation failed"),
        DomainValidationException => (400, "Invalid data"),
        ApplicationValidationException => (400, "Invalid data"),
        ApplicationStateException => (409, "Conflict"),
        DomainStateException => (409, "Invalid state transition"),
        ContentTooLargeException => (413, "Payload Too Large"),
        UnsupportedFileTypeException => (415, "Unsupported Media Type"),
        UserIsBlockedExceptiom => (429, "Too many requests"),
        BadHttpRequestException bad => (bad.StatusCode, bad.StatusCode == 413
            ? "Payload Too Large"
            : "Bad Request"),
        _ => (500, "Internal Server Error")
    };

}