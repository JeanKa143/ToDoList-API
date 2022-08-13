using System.Net;

namespace ToDoList_API.Errors
{
    public class BadRequestError : ApiError
    {
        public BadRequestError(string? message = null, object? errors = null)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message, errors)
        {
        }
    }
}
