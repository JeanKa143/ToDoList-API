using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ToDoList_API.Errors
{
    public class BadRequestError : ApiError
    {
        protected const string DEFAULT_MESSAGE = "One or more fields are invalid";

        public BadRequestError(string message)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message)
        {
        }

        public BadRequestError(ModelStateDictionary errors, string? message = DEFAULT_MESSAGE)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), errors, message)
        {
        }

        public BadRequestError(Dictionary<string, IEnumerable<string>> errors, string? message = DEFAULT_MESSAGE)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), errors, message)
        {
        }
    }
}
