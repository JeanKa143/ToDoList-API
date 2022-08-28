using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ToDoList_API.Errors
{
    public class BadRequestError : ApiError
    {
        protected const string DEFAULT_MESSAGE = "One or more fields are invalid";

        public Dictionary<string, IEnumerable<string>>? Errors { get; private set; }

        public BadRequestError(string message)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message)
        {
        }

        public BadRequestError(ModelStateDictionary errors, string? message = DEFAULT_MESSAGE)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message)
        {
            Errors = ParseModelStateToDictionary(errors);
        }

        public BadRequestError(Dictionary<string, IEnumerable<string>> errors, string? message = DEFAULT_MESSAGE)
            : base((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString(), message)
        {
            Errors = errors;
        }

        private static Dictionary<string, IEnumerable<string>>? ParseModelStateToDictionary(ModelStateDictionary modelState)
        {
            var errors = modelState
                .Where(kvp => kvp.Value!.Errors.Any())
                .AsEnumerable()
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).AsEnumerable());

            return errors;
        }
    }
}
