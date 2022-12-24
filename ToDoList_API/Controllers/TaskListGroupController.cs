using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Errors;
using ToDoList_API.Filters;
using ToDoList_BAL.Models.TaskListGroup;
using ToDoList_BAL.Services;

namespace ToDoList_API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiConventionType(typeof(AppConventions))]
    [ServiceFilter(typeof(ValidateRouteUserIdFilter))]
    [Route("api/user/{userId}/task-list-group")]
    public class TaskListGroupController : ControllerBase
    {
        private readonly TaskListGroupService _taskListGroupService;

        public TaskListGroupController(TaskListGroupService taskListGroupService)
        {
            _taskListGroupService = taskListGroupService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskListGroupDto>> Get([FromRoute] Guid userId, [FromRoute] int id)
        {
            TaskListGroupDto data = await _taskListGroupService.GetByIdAndOwnerIdAsync(id, userId);
            return Ok(data);
        }

        [HttpGet("{id}/detailed")]
        public async Task<ActionResult<DetailedTaskListGroupDto>> GetWithDetails([FromRoute] Guid userId, [FromRoute] int id)
        {
            DetailedTaskListGroupDto data = await _taskListGroupService.GetWithDetailsByIdAndOwnerIdAsync(id, userId);
            return Ok(data);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TaskListGroupDto>>> GetAll([FromRoute] Guid userId)
        {
            IEnumerable<TaskListGroupDto> data = await _taskListGroupService.GetAllByOwnerIdAsync(userId);
            return Ok(data);
        }

        [HttpGet("all/detailed")]
        public async Task<ActionResult<IEnumerable<DetailedTaskListGroupDto>>> GetAllWithDetails([FromRoute] Guid userId)
        {
            IEnumerable<DetailedTaskListGroupDto> data = await _taskListGroupService.GetAllWithDetailsByOwnerIdAsync(userId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<TaskListGroupDto>> Create([FromRoute] Guid userId, [FromBody] CreateTaskListGroupDto createTaskListGroupDto)
        {
            if (!userId.Equals(createTaskListGroupDto.OwnerId))
                return BadRequest(
                    new BadRequestError("The owner id of the DTO cannot be different from the user id of the route"));

            TaskListGroupDto data = await _taskListGroupService.CreateAsync(createTaskListGroupDto);
            return CreatedAtAction(nameof(Get), new { UserId = createTaskListGroupDto.OwnerId, data.Id }, data);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateDtoIdFilter<int>))]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateTaskListGroupDto updateTaskListGroupDto)
        {
            if (!userId.Equals(updateTaskListGroupDto.OwnerId))
                return BadRequest(
                    new BadRequestError("The owner id of the DTO cannot be different from the user id of the route"));

            await _taskListGroupService.UpdateAsync(updateTaskListGroupDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] int id)
        {
            await _taskListGroupService.DeleteAsync(id, userId);
            return NoContent();
        }
    }
}
