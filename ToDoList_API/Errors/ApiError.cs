namespace ToDoList_API.Errors
{
    public class ApiError
    {
        public string StatusDescription { get; private set; }
        public string? Message { get; private set; }
        public int StatusCode { get; private set; }

        public ApiError(int statusCode, string statusDescription, string? message = null)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Message = message;
        }
    }
}
