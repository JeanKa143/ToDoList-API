using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<TaskListGroupDto>> Create([FromBody] CreateTaskListGroupDto createTaskListGroupDto)
        {
            TaskListGroupDto data = await _taskListGroupService.CreateAsync(createTaskListGroupDto);
            return CreatedAtAction(nameof(Get), new { UserId = createTaskListGroupDto.OwnerId, Id = data.Id }, data);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateDtoIdFilter<int>))]
        //Todo: comprobar {userId} con updateTaskListGroupDto.OwnerId
        public async Task<IActionResult> Update([FromBody] UpdateTaskListGroupDto updateTaskListGroupDto)
        {
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
