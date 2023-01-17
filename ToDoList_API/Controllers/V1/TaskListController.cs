using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Errors;
using ToDoList_API.Filters;
using ToDoList_BAL.Models.TaskList;
using ToDoList_BAL.Services;

namespace ToDoList_API.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiConventionType(typeof(AppConventions))]
    [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
    [Route("api/v{version:apiVersion}/users/{userId}/task-list-groups/{groupId}/task-lists")]
    [ApiVersion("1.0")]
    public class TaskListController : ControllerBase
    {
        private readonly TaskListService _taskListService;

        public TaskListController(TaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskListDto>> Get([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int id)
        {
            TaskListDto data = await _taskListService.GetByIdAndGroupIdAsync(userId, groupId, id);
            return Ok(data);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<DetailedTaskListDto>> GetWithDetails([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int id)
        {
            DetailedTaskListDto data = await _taskListService.GetWithDetailsByIdAndGroupIdAsync(userId, groupId, id);
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDto>>> GetAllByGroup([FromRoute] Guid userId, [FromRoute] int groupId)
        {
            IEnumerable<TaskListDto> data = await _taskListService.GetAllByGroupIdAsync(userId, groupId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<TaskListDto>> Create([FromRoute] Guid userId, [FromRoute] int groupId, [FromBody] CreateTaskListDto createTaskListDto)
        {
            if (!groupId.Equals(createTaskListDto.GroupId))
                return BadRequest(
                    new BadRequestError("The group id of the DTO cannot be different from the group id of the route"));

            TaskListDto data = await _taskListService.CreateAsync(userId, createTaskListDto);
            return CreatedAtAction(nameof(Get), new { userId, groupId, data.Id }, data);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateDtoIdFilter<int>))]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromRoute] int groupId, [FromBody] UpdateTaskListDto updateTaskListDto)
        {
            if (!groupId.Equals(updateTaskListDto.GroupId))
                return BadRequest(
                    new BadRequestError("The group id of the DTO cannot be different from the group id of the route"));

            await _taskListService.UpdateAsync(userId, groupId, updateTaskListDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] int groupId, [FromRoute] int id)
        {
            await _taskListService.DeleteAsync(userId, groupId, id);
            return NoContent();
        }
    }
}
