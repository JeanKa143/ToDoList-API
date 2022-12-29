using AutoMapper;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskList;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Services
{
    public class TaskListService
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly ITaskListGroupRepository _taskListGroupRepository;
        private readonly IMapper _mapper;

        public TaskListService(ITaskListRepository taskListRepository, ITaskListGroupRepository taskListGroupRepository, IMapper mapper)
        {
            _taskListRepository = taskListRepository;
            _taskListGroupRepository = taskListGroupRepository;
            _mapper = mapper;
        }

        public async Task<TaskListDto> GetByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            TaskList? entity = await _taskListRepository.GetByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), id);

            return _mapper.Map<TaskListDto>(entity);
        }

        public async Task<DetailedTaskListDto> GetWithDetailsByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            TaskList? entity = await _taskListRepository.GetWithDetailsByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), id);

            return _mapper.Map<DetailedTaskListDto>(entity);
        }

        public async Task<IEnumerable<TaskListDto>> GetAllByOwnerIdAndGroupIdAsync(Guid ownerId, int groupId)
        {
            IEnumerable<TaskList> entities = await _taskListRepository.GetAllByOwnerIdAndGroupIdAsync(ownerId, groupId);
            return _mapper.Map<IEnumerable<TaskListDto>>(entities);
        }

        public async Task<TaskListDto> CreateAsync(Guid ownerId, CreateTaskListDto createTaskListDto)
        {
            var taskListGroup = await _taskListGroupRepository.GetByIdAndOwnerIdAsync(createTaskListDto.GroupId, ownerId);

            if (taskListGroup is null)
                throw new NotFoundException(nameof(TaskListGroup), createTaskListDto.GroupId);

            TaskList entity = _mapper.Map<TaskList>(createTaskListDto);
            await _taskListRepository.CreateAsync(entity);
            return _mapper.Map<TaskListDto>(entity);
        }

        public async Task UpdateAsync(UpdateTaskListDto updateTaskListDto, Guid ownerId)
        {
            TaskList? entity = await _taskListRepository.GetByIdAndOwnerIdAsync(updateTaskListDto.Id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), updateTaskListDto.Id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot update default task list");

            _mapper.Map(updateTaskListDto, entity);
            await _taskListRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id, Guid ownerId)
        {
            TaskList? entity = await _taskListRepository.GetByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot delete default task list");

            await _taskListRepository.DeleteAsync(entity);
        }
    }
}
