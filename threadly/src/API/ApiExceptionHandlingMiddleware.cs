using ApplyProjectStudio.Domain.Workspaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplyProjectStudio.API;

public sealed class ApiExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ApiExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidWorkspaceNameException exception)
        {
            await WriteProblemAsync(context, StatusCodes.Status400BadRequest, "Invalid workspace name", exception.Message);
        }
        catch (WorkspaceAlreadyExistsException exception)
        {
            await WriteProblemAsync(context, StatusCodes.Status409Conflict, "Workspace already exists", exception.Message);
        }
    }

    private static async Task WriteProblemAsync(HttpContext context, int statusCode, string title, string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
