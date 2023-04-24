using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;

namespace LoanProject.Web.Infrastructure.Helper
{
    public static class ExceptionValidator
    {
        public static void ValidateException(HttpContext context)
        {
            var statusCode = context.Response.StatusCode;
            var sw = Stopwatch.StartNew();
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
    }
}
