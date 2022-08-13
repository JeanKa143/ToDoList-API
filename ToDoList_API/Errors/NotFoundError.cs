using System.Net;

namespace ToDoList_API.Errors
{
    public class NotFoundError : ApiError
    {
        public NotFoundError(string? message = null) :
            base((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), message)
        {
        }
    }
}
