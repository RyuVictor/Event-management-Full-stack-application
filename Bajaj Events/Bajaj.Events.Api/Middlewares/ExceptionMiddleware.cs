using Bajaj.Events.Models;
using System.Text;
using System.Text.Json;

namespace Bajaj.Events.Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly HttpClient _httpClient;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _logger.LogInformation($"In try status code: {context.Response.StatusCode}");
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred: {ex}");

                // Handle the exception
                await HandleException(context, ex);
            }
            _logger.LogInformation($"Final status code: {context.Response.StatusCode}");
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            // Log the exception and set the appropriate status code
            int statusCode = StatusCodes.Status500InternalServerError;
            var errorMessage = ex.Message;
            var source = ex.Source;
            var stackTrace = ex.StackTrace;
            _logger.LogError($"Error: {errorMessage}, Source: {source}, Stack Trace: {stackTrace}");

            // Define the target API endpoint
            var targetApiUrl = "https://localhost:7267/api/Log";

            // Create the request body
            var requestBody = new
            {
                StatusCode = statusCode,
                Message = errorMessage,
                Source = source,
                StackTrace = stackTrace
            };

            // Serialize the request body
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request to the target API
            var response = await _httpClient.PostAsync(targetApiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to call target API: {response.StatusCode}");
            }

            // Set the response status code and content type
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            // Write the error message to the response body
            var errorResponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = errorMessage,
                Source = source,
                StackTrace = stackTrace
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

    }

    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
