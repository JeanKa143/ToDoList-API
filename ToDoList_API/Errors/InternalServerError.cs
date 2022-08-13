using System.Net;

namespace ToDoList_API.Errors
{
    public class InternalServerError : ApiError
    {
        public InternalServerError(string? message = null)
            : base((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), message)
        {
        }
    }
}
