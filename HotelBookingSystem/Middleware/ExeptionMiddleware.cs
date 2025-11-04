
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HotelBookingSystem.Middleware;
public class ExeptionMiddleware(ILogger<ExeptionMiddleware> logger, ProblemDetailsFactory problemDetailsFactory) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
        var correlationId = context.Request.Headers["X-Correlation-Id"].ToString();

        var messageDetails = $"Kindly contact admin with your error code: {correlationId}";
        var detail = environment.IsDevelopment()
            ? $"{exception.Message} {exception.StackTrace} {messageDetails}"
            : messageDetails;

        var statusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode,
            title: "An unexpected error occurred.",
            type: "https://httpstatuses.com/500",
            detail);

        problemDetails.Extensions.Add("correlationId", correlationId);
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.StatusCode = statusCode;
        context.Response.Headers.Append("X-Correlation-Id", correlationId); ;

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        logger.LogError(exception, $"An unhandled exception occurred. {correlationId}");

        return true;
    }
}