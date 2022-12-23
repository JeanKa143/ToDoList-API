using System.Net;

namespace ToDoList_API.Errors
{
    public class ForbiddenError : ApiError
    {
        public ForbiddenError(string? message = null) :
            base((int)HttpStatusCode.Forbidden, HttpStatusCode.Forbidden.ToString(), message)
        {
        }
    }
}
