namespace GeometryCurrencyApp;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        _logger.Log(LogLevel.Information, $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}");

        using (var buffer = new MemoryStream())
        {
            var responseBodyStream = context.Response.Body;
            context.Response.Body = buffer;


            await _next.Invoke(context);


            buffer.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(buffer);
            using (var bufferReader = new StreamReader(buffer))
            {
                string responseBody = await bufferReader.ReadToEndAsync();

                buffer.Seek(0, SeekOrigin.Begin);

                await buffer.CopyToAsync(responseBodyStream);
                context.Response.Body = responseBodyStream;

                _logger.Log(LogLevel.Information, $"Response code: {context.Response.StatusCode}, response body: {responseBody}");
            }
        } 
    }

}
