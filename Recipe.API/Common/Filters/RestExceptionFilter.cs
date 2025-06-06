using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recipe.Application.Exceptions;

namespace Recipe.API.Common.Filters;

public class RestExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<RestExceptionFilter> _logger;
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public RestExceptionFilter(ILogger<RestExceptionFilter> logger)
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(RepositoryException), HandleBaseException }
        };
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type](context);
            return;
        }

        if (_exceptionHandlers.ContainsKey(type.BaseType))
        {
            _exceptionHandlers[type.BaseType].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request."
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        _logger.LogError(context.Exception, "An exception occurred, Message: {message}, Exception: {ex}",
            context.Exception.Message, context.Exception);
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleBaseException(ExceptionContext context)
    {
        if (context.Exception is not RepositoryException exception) return;
        var statusCode = StatusCodes.Status400BadRequest;
        if (exception.Code == "401")
        {
            statusCode = StatusCodes.Status401Unauthorized;
        }

        var response = new
        {
            message = exception.Message,
            timestamp = DateTime.UtcNow,
            code = statusCode,
            identifier = exception.Description
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = statusCode
        };

        _logger.LogError(context.Exception, "An exception occurred, Code: {code}, Message: {message}", exception.Code,
            exception.Message);

        context.ExceptionHandled = true;
    }
}