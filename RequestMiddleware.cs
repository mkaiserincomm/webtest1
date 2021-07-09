using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Prometheus;

public class RequestMiddleware  
{  
    private readonly RequestDelegate _next;  
    private readonly ILogger _logger;  
  
    public RequestMiddleware(  
        RequestDelegate next  
        , ILoggerFactory loggerFactory  
        )  
    {  
        this._next = next;  
        this._logger = loggerFactory.CreateLogger<RequestMiddleware>();  
    }  
      
    public async Task Invoke(HttpContext httpContext)  
    {  
        var path = httpContext.Request.Path.Value;  
        var method = httpContext.Request.Method;  
  
        var counter = Metrics.CreateCounter("http_request_stats", "HTTP Request Statistics", new CounterConfiguration  
        {  
            LabelNames = new[] { "path", "method", "status", "elapsedms" }  
        });  
  
        var statusCode = 200;          
  
        var stopwatch = Stopwatch.StartNew();
        try  
        {  
            await _next.Invoke(httpContext); 
            stopwatch.Stop(); 
        }  
        catch (Exception)  
        {  
            stopwatch.Stop(); 
            statusCode = 500;  
            counter.Labels(path, method, statusCode.ToString(), stopwatch.ElapsedMilliseconds.ToString()).Inc();  
  
            throw;  
        }  
          
        if (path != "/metrics")  
        {  
            statusCode = httpContext.Response.StatusCode;  
            counter.Labels(path, method, statusCode.ToString(), stopwatch.ElapsedMilliseconds.ToString()).Inc();  
        }  
    }  
}  
  
public static class RequestMiddlewareExtensions  
{          
    public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)  
    {  
        return builder.UseMiddleware<RequestMiddleware>();  
    }  
}  