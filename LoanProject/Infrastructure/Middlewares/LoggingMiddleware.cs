﻿using LoanProject.Web.Infrastructure.Helper;
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
            ExceptionValidator.ValidateException(context);
            var statusCode = context.Response.StatusCode;
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