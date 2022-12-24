using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList_API.Errors;
using ToDoList_BAL.Models;

namespace ToDoList_API.Filters
{
    public class ValidateDtoIdFilter<T> : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IModelDto<T>? modelDto = context.ActionArguments.SingleOrDefault(p => p.Value is IModelDto<T>).Value as IModelDto<T>;
            var routeId = context.RouteData.Values["id"] as string;

            if (modelDto is null || routeId is null)
                return;

            if (modelDto.Id?.ToString() != routeId)
                context.Result = new BadRequestObjectResult(
                    new BadRequestError("The id of the DTO cannot be different from the id of the route"));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
