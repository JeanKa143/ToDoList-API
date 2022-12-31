using AutoMapper;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskItem;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Services
{
    public class TaskItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskItemDto> GetByIdAndListIdAsync(Guid ownerId, int groupId, int listId, int id)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, listId);

            TaskItem? entity = await _unitOfWork.TaskItems.GetByIdAndListIdAsync(id, listId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskItem), id);

            return _mapper.Map<TaskItemDto>(entity);
        }

        public async Task<DetailedTaskItemDto> GetWithDetailsByIdAndListIdAsync(Guid ownerId, int groupId, int listId, int id)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, listId);

            TaskItem? entity = await _unitOfWork.TaskItems.GetWithDetailsByIdAndListIdAsync(id, listId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskItem), id);

            return _mapper.Map<DetailedTaskItemDto>(entity);
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllByListIdAsync(Guid ownerId, int groupId, int listId)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, listId);

            IEnumerable<TaskItem> entities = await _unitOfWork.TaskItems.GetAllByListIdAsync(listId);
            return _mapper.Map<IEnumerable<TaskItemDto>>(entities);
        }

        public async Task<TaskItemDto> CreateAsync(Guid ownerId, int groupId, CreateTaskItemDto createTaskItemDto)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, createTaskItemDto.TaskListId);

            TaskItem entity = _mapper.Map<TaskItem>(createTaskItemDto);
            _unitOfWork.TaskItems.Create(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TaskItemDto>(entity);
        }

        public async Task UpdateAsync(Guid ownerId, int groupId, UpdateTaskItemDto updateTaskItemDto)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, updateTaskItemDto.TaskListId);

            TaskItem? entity = await _unitOfWork.TaskItems.GetByIdAndListIdAsync(updateTaskItemDto.Id, updateTaskItemDto.TaskListId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskItem), updateTaskItemDto.Id);

            _mapper.Map(updateTaskItemDto, entity);
            _unitOfWork.TaskItems.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task MarkAsDoneAsync(Guid ownerId, int groupId, int listId, int id)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, listId);

            TaskItem? entity = await _unitOfWork.TaskItems.GetByIdAndListIdAsync(id, listId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskItem), id);

            entity.IsDone = true;
            _unitOfWork.TaskItems.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UnMarkAsDoneAsync(Guid ownerId, int groupId, int listId, int id)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, listId);

            TaskItem? entity = await _unitOfWork.TaskItems.GetByIdAndListIdAsync(id, listId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskItem), id);

            entity.IsDone = false;
            _unitOfWork.TaskItems.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid ownerId, int groupId, int listId, int id)
        {
            await CheckIfTaskListExistAsync(ownerId, groupId, listId);

            TaskItem? entity = await _unitOfWork.TaskItems.GetByIdAndListIdAsync(id, listId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskItem), id);

            _unitOfWork.TaskItems.Delete(entity);
            await _unitOfWork.SaveAsync();
        }


        private async Task CheckIfTaskListExistAsync(Guid ownerId, int groupId, int listId)
        {
            if (!await _unitOfWork.TaskListGroups.IsAnyWithOwnerIdAndGroupIdAsync(ownerId, groupId))
                throw new NotFoundException(nameof(TaskListGroup), groupId);

            if (!await _unitOfWork.TaskLists.IsAnyWithGroupIdAndListIdAsync(groupId, listId))
                throw new NotFoundException(nameof(TaskList), listId);
        }
    }
}
