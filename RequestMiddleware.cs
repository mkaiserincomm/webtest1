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
        var elapsedMsHist = Metrics.CreateHistogram("http_request_elapsedms_hist", "HTTP Request Elapsed Ms Histogram", new HistogramConfiguration { LabelNames = new[] { "path", "method"}, Buckets = Histogram.ExponentialBuckets(.25,2,20) });
        var statusCounter = Metrics.CreateCounter("http_request_count", "HTTP Request Status Count", new CounterConfiguration { LabelNames = new[] { "path", "method", "status"} });          
        var elapsedMsCounter = Metrics.CreateGauge("http_request_elapsedms", "HTTP Request Elapsed Ms", new GaugeConfiguration { LabelNames = new[] { "path", "method"} });  
  
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
            statusCounter.Labels(path, method, statusCode.ToString()).Inc();
            elapsedMsCounter.Labels(path, method).Set(stopwatch.ElapsedMilliseconds);
            elapsedMsHist.Observe(stopwatch.ElapsedMilliseconds);
  
            throw;  
        }  
          
        if (path != "/metrics")  
        {  
            statusCode = httpContext.Response.StatusCode;  
            statusCounter.Labels(path, method, statusCode.ToString()).Inc();
            elapsedMsCounter.Labels(path, method).Set(stopwatch.ElapsedMilliseconds);
            elapsedMsHist.Observe(stopwatch.ElapsedMilliseconds);
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