using Microsoft.AspNetCore.Http.Features;
using Serilog;
using System.Diagnostics;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();

        try
        {
            await _next(context);

            sw.Stop();

            var statusCode = context.Response.StatusCode;

            if (statusCode >= 500)
            {
                Serilog.Log.Error("{StatusCode} {ReasonPhrase} for request {Method} {Url} ({ElapsedMilliseconds}ms)",
                    statusCode, context.Features.Get<IHttpResponseFeature>().ReasonPhrase,
                    context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
            }
            else if (statusCode >= 400)
            {
                Serilog.Log.Warning("{StatusCode} {ReasonPhrase} for request {Method} {Url} ({ElapsedMilliseconds}ms)",
                    statusCode, context.Features.Get<IHttpResponseFeature>().ReasonPhrase,
                    context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
            }
            else
            {
                Serilog.Log.Information("{Method} {Url} responded {StatusCode} ({ElapsedMilliseconds}ms)",
                    context.Request.Method, context.Request.Path, statusCode, sw.ElapsedMilliseconds);
            }
        }
        catch (Exception ex)
        {
            sw.Stop();

            Serilog.Log.Error(ex, "{Method} {Url} threw an exception after {ElapsedMilliseconds}ms",
                context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);

            throw;
        }
    }
}