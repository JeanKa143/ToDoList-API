using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList_API.Errors;
using ToDoList_BAL.Models;

namespace ToDoList_API.Filters
{
    public class ValidateUserIdAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid? routeUserId = context.ActionArguments.SingleOrDefault(p => p.Value is Guid).Value as Guid?;
            IModelDTO<Guid>? modelDto = context.ActionArguments.SingleOrDefault(p => p.Value is IModelDTO<Guid>).Value as IModelDTO<Guid>;
            var tokenUserId = GetUserIdFromToken(context.HttpContext);

            if (routeUserId is null || modelDto is null || tokenUserId == Guid.Empty)
            {
                return;
            }

            if (routeUserId != modelDto.Id || routeUserId != tokenUserId)
            {
                context.Result = new BadRequestObjectResult(new BadRequestError("Invalid user id"));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private static Guid GetUserIdFromToken(HttpContext httpContext)
        {
            var idClaim = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == "userId");
            return idClaim is not null ? Guid.Parse(idClaim.Value) : Guid.Empty;
        }
    }
}
