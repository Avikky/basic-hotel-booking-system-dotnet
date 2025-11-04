namespace HotelBookingSystem.Middleware;

public class ApplicationMiddleware(ILogger<ApplicationMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Request.Headers.Append("X-Correlation-Id", Guid.NewGuid().ToString());
        logger.LogInformation("Middleware - Before request");

        await next(context);

        logger.LogInformation("Middleware - After request");
    }
}