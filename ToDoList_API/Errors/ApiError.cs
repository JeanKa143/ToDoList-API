using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoList_API.Errors
{
    public class ApiError
    {
        public string StatusDescription { get; private set; }
        public string? Message { get; private set; }
        public int StatusCode { get; private set; }
        public Dictionary<string, IEnumerable<string>>? Errors { get; private set; }

        public ApiError(int statusCode, string statusDescription, string? message = null)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            Message = message;
        }

        public ApiError(int statusCode, string statusDescription, ModelStateDictionary errors, string? message = null)
            : this(statusCode, statusDescription, message)
        {
            Errors = ParseModelStateToDictionary(errors);
        }

        public ApiError(int statusCode, string statusDescription, Dictionary<string, IEnumerable<string>> errors, string? message = null)
            : this(statusCode, statusDescription, message)
        {
            Errors = errors;
        }

        private static Dictionary<string, IEnumerable<string>>? ParseModelStateToDictionary(ModelStateDictionary modelState)
        {
            var errors = modelState.AsEnumerable().ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).AsEnumerable()
            );

            return errors;
        }
    }
}
