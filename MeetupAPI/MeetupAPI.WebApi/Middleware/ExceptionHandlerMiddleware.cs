using FluentValidation;
using System.Text.Json;
using System.Net;
using MeetupAPI.Application.Common.Exceptions;

namespace MeetupAPI.WebApi.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            HandleExceptionAsync(context, exception);
        }

    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var responseMessage = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                responseMessage = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        if (responseMessage == string.Empty)
            responseMessage = JsonSerializer.Serialize(new {error = exception.Message});

        return context.Response.WriteAsync(responseMessage);
    }
}