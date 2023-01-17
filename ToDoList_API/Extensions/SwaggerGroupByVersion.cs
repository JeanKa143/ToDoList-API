using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ToDoList_API.Extensions
{
    public class SwaggerGroupByVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            string? controllerNamespace = controller.ControllerType.Namespace;
            string? APIVersion = controllerNamespace?.Split('.').Last().ToLower();
            controller.ApiExplorer.GroupName = APIVersion;
        }
    }
}
