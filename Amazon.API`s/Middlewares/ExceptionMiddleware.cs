using Amazon.API_s.Errors;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

namespace Amazon.API_s.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); //log error in database

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response =_env.IsDevelopment() ?
                    new ApiExceptionResponse(500, ex.Message,ex.InnerException?.Message) // law howa fi el development hnrga3 da
                    : new ApiExceptionResponse(500); // law howa production hnrga3 da fi el database

                var options =new JsonSerializerOptions(){ PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

                var returned = JsonSerializer.Serialize(response,options);
                
                await context.Response.WriteAsync(returned);
            }
        }
    }
}
