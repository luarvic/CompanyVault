using System.Net;
using Newtonsoft.Json;

namespace CompanyVault.WebApi.Middlewares;

/// <summary>
/// A custom middleware that handles exceptions and returns appropriate response.
/// </summary>
/// <typeparam name="ExceptionHandlerMiddleware"></typeparam>
public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public (HttpStatusCode Code, string Message) GetResponse(Exception exception)
    {
        var code = exception switch
        {
            InvalidOperationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };
        return (code, JsonConvert.SerializeObject(exception.Message));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error in {Method} {Context}", context.Request.Method, context.Request.Path.Value);

            var response = context.Response;
            response.ContentType = "application/json";
            var (status, message) = GetResponse(e);
            response.StatusCode = (int)status;
            await response.WriteAsync(message);
        }
    }
}
