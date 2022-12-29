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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TaskListGroupService(ITaskListGroupRepository taskListGroupRepository, IUserRepository userRepository, IMapper mapper)
        {
            _taskListGroupRepository = taskListGroupRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<TaskListGroupDto> GetByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), id);

            return _mapper.Map<TaskListGroupDto>(entity);
        }

        public async Task<DetailedTaskListGroupDto> GetWithDetailsByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetWithDetailsByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), id);

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
            var user = await _userRepository.GetByIdAsync(createTaskListGroupDto.OwnerId.ToString());

            if (user is null) //Todo: Test condition
                throw new NotFoundException("User", createTaskListGroupDto.OwnerId);

            TaskListGroup entity = _mapper.Map<TaskListGroup>(createTaskListGroupDto);
            await _taskListGroupRepository.CreateAsync(entity);
            return _mapper.Map<TaskListGroupDto>(entity);
        }

        public async Task UpdateAsync(UpdateTaskListGroupDto updateTaskListGroupDto)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetByIdAndOwnerIdAsync(updateTaskListGroupDto.Id, updateTaskListGroupDto.OwnerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), updateTaskListGroupDto.Id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot update default task list group");

            _mapper.Map(updateTaskListGroupDto, entity);
            await _taskListGroupRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id, Guid ownerId)
        {
            TaskListGroup? entity = await _taskListGroupRepository.GetByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot delete default task list group");

            await _taskListGroupRepository.DeleteAsync(entity);
        }
    }
}
