using AutoMapper;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskListGroup;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Services
{
    public class TaskListGroupService
    {
        private readonly ITaskListGroupRepository _taskListGroupRepository;
        private readonly IMapper _mapper;

        public TaskListGroupService(ITaskListGroupRepository taskListGroupRepository, IMapper mapper)
        {
            _taskListGroupRepository = taskListGroupRepository;
            _mapper = mapper;
        }

        public async Task<TaskListGroupDto> GetByIdAsync(int taskListGroupId, Guid ownerId)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetByIdAsync(taskListGroupId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), taskListGroupId);

            if (!entity.OwnerId.Equals(ownerId.ToString()))
                throw new ForbiddenException(nameof(TaskListGroup), taskListGroupId);

            return _mapper.Map<TaskListGroupDto>(entity);
        }

        public async Task<DetailedTaskListGroupDto> GetWithDetailsAsync(int taskListGroupId, Guid ownerId)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetWithDetailsAsync(taskListGroupId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), taskListGroupId);

            if (!entity.OwnerId.Equals(ownerId.ToString()))
                throw new ForbiddenException(nameof(TaskListGroup), taskListGroupId);

            return _mapper.Map<DetailedTaskListGroupDto>(entity);
        }

        public async Task<IEnumerable<TaskListGroupDto>> GetAllByOwnerIdAsync(Guid ownerId)
        {
            IEnumerable<TaskListGroup> entities = await _taskListGroupRepository.GetAllByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<TaskListGroupDto>>(entities);
        }

        public async Task<IEnumerable<DetailedTaskListGroupDto>> GetAllWithDetailsByOwnerIdAsync(Guid ownerId)
        {
            IEnumerable<TaskListGroup> entities = await _taskListGroupRepository.GetAllWithDetailsByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<DetailedTaskListGroupDto>>(entities);
        }

        public async Task<TaskListGroupDto> CreateAsync(CreateTaskListGroupDto createTaskListGroupDto)
        {
            TaskListGroup entity = _mapper.Map<TaskListGroup>(createTaskListGroupDto);
            await _taskListGroupRepository.CreateAsync(entity);
            return _mapper.Map<TaskListGroupDto>(entity);
        }

        public async Task UpdateAsync(UpdateTaskListGroupDto updateTaskListGroupDto)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetByIdAsync(updateTaskListGroupDto.Id);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), updateTaskListGroupDto.Id);

            if (!entity.OwnerId.Equals(updateTaskListGroupDto.OwnerId.ToString()))
                throw new ForbiddenException(nameof(TaskListGroup), updateTaskListGroupDto.Id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot update default task list group");

            _mapper.Map(updateTaskListGroupDto, entity);
            await _taskListGroupRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int taskListGroupId, Guid ownerId)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetByIdAsync(taskListGroupId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), taskListGroupId);

            if (!entity.OwnerId.Equals(ownerId.ToString()))
                throw new ForbiddenException(nameof(TaskListGroup), taskListGroupId);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot delete default task list group");

            await _taskListGroupRepository.DeleteAsync(entity);
        }
    }
}
