using System.Net;

namespace ToDoList_API.Errors
{
    public class UnauthorizedError : ApiError
    {
        public UnauthorizedError(string? message = null)
            : base((int)HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), message)
        {
        }
    }
}
