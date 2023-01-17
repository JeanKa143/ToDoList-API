using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoList_API.Controllers.V1;
using ToDoList_API.Errors;

namespace ToDoList_API.Filters
{
    public class ValidateRouteUserIdFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerType = context.Controller.GetType();

            if ((controllerType.Equals(typeof(AccountController))
                ? context.RouteData.Values["id"]
                : context.RouteData.Values["userId"]) is not string routeUserId)
                return;

            routeUserId = routeUserId.ToLower();

            Guid tokenUserId = GetUserIdFromToken(context.HttpContext);

            if (tokenUserId == Guid.Empty)
                return;

            if (!routeUserId.Equals(tokenUserId.ToString()))
                context.Result = new BadRequestObjectResult(
                    new BadRequestError("The user id of the route cannot be different from the user id of the token"));
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
