using System.Net;
using TutorDesk.Common.Exceptions;

namespace TutorDesk.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode = 500;
        string message = "An unexpected error occurred.";

        if (exception is AppException appEx)
        {
            statusCode = appEx.StatusCode;
            message = appEx.Message;
        }
        else if (exception is UnauthorizedAccessException)
        {
            statusCode = 401;
            message = "Unauthorized";
        }

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            success = false,
            message
        });
    }
}