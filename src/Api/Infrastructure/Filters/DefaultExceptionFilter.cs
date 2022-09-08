using AErmilov.NumbersIntoWords.Api.Infrastructure.Problems;
using AErmilov.NumbersIntoWords.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;

namespace AErmilov.NumbersIntoWords.Api.Infrastructure.Filters;

internal sealed class DefaultExceptionFilter : IExceptionFilter
{
    private readonly ProblemDetailsFactory problemDetailsFactory;
    private readonly ILogger logger;

    public DefaultExceptionFilter(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<DefaultExceptionFilter> logger)
    {
        this.problemDetailsFactory = problemDetailsFactory;
        this.logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not ServiceException serviceException)
        {
            // Handle this exception as server or infrastructure related
            var commonProblemDetails = problemDetailsFactory.CreateProblemDetails(context.HttpContext, detail: context.Exception.Message);
            SetStatusAndResponse(context, commonProblemDetails);

            LogProblem(commonProblemDetails, isError: true);
            return;
        }

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            context.HttpContext,
            StatusCodes.Status422UnprocessableEntity,
            string.IsNullOrWhiteSpace(serviceException.ShortDescription)
                ? "Unable to perform an operation"
                : serviceException.ShortDescription,
            string.IsNullOrWhiteSpace(serviceException.ErrorCode)
                ? DefaultProblemDetailTypes.BusinessLogicException
                : serviceException.ErrorCode,
            serviceException.Message,
            instance: null
        );

        var statusCode = serviceException switch
        {
            OutOfRangeNumberException _ => StatusCodes.Status400BadRequest,
            TooManyDecimalPlacesException _ => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status422UnprocessableEntity
        };

        SetStatusAndResponse(context, problemDetails, statusCode);
        LogProblem(problemDetails, isError: false);
    }

    private void SetStatusAndResponse(ExceptionContext context, ProblemDetails problemDetails, int? overridenStatusCode = null)
    {
        problemDetails.Status = overridenStatusCode ?? problemDetails.Status;

        if (problemDetails.Status.HasValue)
        {
            context.HttpContext.Response.StatusCode = problemDetails.Status.Value;
        }

        context.Result = new ObjectResult(problemDetails) { StatusCode = overridenStatusCode };

        context.ExceptionHandled = true;
    }

    private void LogProblem(ProblemDetails problemDetails, bool isError)
    {
        var message = "An error occurred while processing your request. Uri: {RequestUri}. Message: {ErrorMessage}. {Model}";

        if (isError)
        {
            logger.LogError(message, problemDetails.Instance, problemDetails.Detail, JsonSerializer.Serialize(problemDetails));
        }
        else
        {
            logger.LogWarning(message, problemDetails.Instance, problemDetails.Detail, problemDetails);
        }
    }
}