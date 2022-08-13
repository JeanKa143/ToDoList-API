namespace ToDoList_API.Errors
{
    public class ApiError
    {
        public string StatusDescription { get; private set; }
        public string? Message { get; private set; }
        public int StatusCode { get; private set; }
        public object? Errors { get; private set; }

        public ApiError(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string? message = null, object? errors = null)
            : this(statusCode, statusDescription)
        {
            Message = message;
            Errors = errors;
        }
    }
}
