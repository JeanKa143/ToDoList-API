using Newtonsoft.Json;
using System.Net;
using ToDoList_BAL.Exceptions;

namespace ToDoList_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var errorMessage = ex.Message;

            var statusCode = ex switch
            {
                BadRequestException => HttpStatusCode.BadRequest,
                LoginException => HttpStatusCode.Unauthorized,
                NotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(ex, "Something went wrong while processing {RequestPath}", context.Request.Path);
                errorMessage = "An unexpected error occurred";
            }

            var errorDetails = new ErrorDetails
            {
                ErrorType = statusCode.ToString(),
                ErrorMessage = errorMessage
            };

            string response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(response);
        }

    }

    public class ErrorDetails
    {
        public string ErrorType { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
