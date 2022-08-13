using Newtonsoft.Json;
using ToDoList_API.Errors;
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
            var apiError = CreateApiError(context, ex);
            string response = JsonConvert.SerializeObject(apiError);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = apiError.StatusCode;
            return context.Response.WriteAsync(response);
        }

        private ApiError CreateApiError(HttpContext context, Exception ex)
        {
            ApiError apiError;

            switch (ex)
            {
                case NotFoundException:
                    apiError = new NotFoundError(ex.Message);
                    break;
                case BadRequestException:
                    apiError = new BadRequestError(ex.Message);
                    break;
                case LoginException:
                    apiError = new UnauthorizedError(ex.Message);
                    break;
                default:
                    _logger.LogError(ex, "Something went wrong while processing {RequestPath}", context.Request.Path);
                    apiError = new InternalServerError("An unexpected error occurred");
                    break;
            }

            return apiError;
        }
    }
}
