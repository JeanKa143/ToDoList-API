using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Errors;
using ToDoList_API.Filters;
using ToDoList_BAL.Models.TaskStep;
using ToDoList_BAL.Services;

namespace ToDoList_API.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiConventionType(typeof(AppConventions))]
    [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
    [Route("api/v{version:apiVersion}/users/{userId}/task-list-groups/{groupId}/task-lists/{listId}/tasks/{taskId}/steps")]
    [ApiVersion("1.0")]
    public class TaskStepController : ControllerBase
    {
        private readonly TaskStepService _taskStepService;

        public TaskStepController(TaskStepService taskStepService)
        {
            _taskStepService = taskStepService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskStepDto>>> GetAllByTaskItem([FromRoute] Guid userId, [FromRoute] int groupId,
            [FromRoute] int listId, [FromRoute] int taskId)
        {
            IEnumerable<TaskStepDto> data = await _taskStepService.GetAllByTaskItemIdAsync(userId, groupId, listId, taskId);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrCreateTaskSteps([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId,
            [FromRoute] int taskId, [FromBody] List<CreateOrUpdateTaskStepDto> taskStepDtos)
        {
            if (taskStepDtos.Count is 0)
                return BadRequest(new BadRequestError("The list of task steps cannot be empty"));

            await _taskStepService.CreateOrUpdateTaskStepsAsync(userId, groupId, listId, taskId, taskStepDtos);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId,
            [FromRoute] int taskId, [FromBody] int[] ids)
        {
            if (ids.Length is 0)
                return BadRequest(new BadRequestError("The list of task step ids cannot be empty"));

            await _taskStepService.DeleteAsync(userId, groupId, listId, taskId, ids);
            return NoContent();
        }
    }
}
