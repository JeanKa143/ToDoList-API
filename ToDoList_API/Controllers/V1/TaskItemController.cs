using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Errors;
using ToDoList_API.Filters;
using ToDoList_BAL.Models.TaskItem;
using ToDoList_BAL.Services;

namespace ToDoList_API.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiConventionType(typeof(AppConventions))]
    [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
    [Route("api/v{version:apiVersion}/users/{userId}/task-list-groups/{groupId}/task-lists/{listId}/tasks")]
    [ApiVersion("1.0")]
    public class TaskItemController : ControllerBase
    {
        private readonly TaskItemService _taskItemService;

        public TaskItemController(TaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> Get([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId, [FromRoute] int id)
        {
            TaskItemDto data = await _taskItemService.GetByIdAndListIdAsync(userId, groupId, listId, id);
            return Ok(data);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<DetailedTaskItemDto>> GetWithDetails([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId, [FromRoute] int id)
        {
            DetailedTaskItemDto data = await _taskItemService.GetWithDetailsByIdAndListIdAsync(userId, groupId, listId, id);
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAllByList([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId)
        {
            IEnumerable<TaskItemDto> data = await _taskItemService.GetAllByListIdAsync(userId, groupId, listId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId, [FromBody] CreateTaskItemDto createTaskItemDto)
        {
            if (!listId.Equals(createTaskItemDto.TaskListId))
                return BadRequest(
                    new BadRequestError("The list id of the DTO cannot be different from the list id of the route"));

            TaskItemDto data = await _taskItemService.CreateAsync(userId, groupId, createTaskItemDto);
            return CreatedAtAction(nameof(Get), new { userId, groupId, listId, data.Id }, data);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateDtoIdFilter<int>))]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId, [FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            if (!listId.Equals(updateTaskItemDto.TaskListId))
                return BadRequest(
                    new BadRequestError("The list id of the DTO cannot be different from the list id of the route"));

            await _taskItemService.UpdateAsync(userId, groupId, updateTaskItemDto);
            return NoContent();
        }

        [HttpPut("{id}/mark-as-done")]
        public async Task<IActionResult> UpdateMarkAsDone([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId, [FromRoute] int id)
        {
            await _taskItemService.MarkAsDoneAsync(userId, groupId, listId, id);
            return NoContent();
        }

        [HttpPut("{id}/mark-as-not-done")]
        public async Task<IActionResult> UpdateMarkAsNotDone([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId, [FromRoute] int id)
        {
            await _taskItemService.UnMarkAsDoneAsync(userId, groupId, listId, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int listId, [FromRoute] int id)
        {
            await _taskItemService.DeleteAsync(userId, groupId, listId, id);
            return NoContent();
        }
    }
}
