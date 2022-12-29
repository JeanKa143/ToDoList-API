using AutoMapper;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskList;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Services
{
    public class TaskListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskListDto> GetByIdAndGroupIdAsync(Guid ownerId, int groupId, int id)
        {
            await CheckIfGroupWithOwnerExistsAsync(ownerId, groupId);

            TaskList? entity = await _unitOfWork.TaskLists.GetByIdAndGroupIdAsync(id, groupId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), id);

            return _mapper.Map<TaskListDto>(entity);
        }

        public async Task<DetailedTaskListDto> GetWithDetailsByIdAndGroupIdAsync(Guid ownerId, int groupId, int id)
        {
            await CheckIfGroupWithOwnerExistsAsync(ownerId, groupId);

            TaskList? entity = await _unitOfWork.TaskLists.GetWithDetailsByIdAndGroupIdAsync(id, groupId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), id);

            return _mapper.Map<DetailedTaskListDto>(entity);
        }

        public async Task<IEnumerable<TaskListDto>> GetAllByGroupIdAsync(Guid ownerId, int groupId)
        {
            await CheckIfGroupWithOwnerExistsAsync(ownerId, groupId);

            IEnumerable<TaskList> entities = await _unitOfWork.TaskLists.GetAllByGroupIdAsync(groupId);
            return _mapper.Map<IEnumerable<TaskListDto>>(entities);
        }

        public async Task<TaskListDto> CreateAsync(Guid ownerId, CreateTaskListDto createTaskListDto)
        {
            await CheckIfGroupWithOwnerExistsAsync(ownerId, createTaskListDto.GroupId);

            TaskList entity = _mapper.Map<TaskList>(createTaskListDto);
            _unitOfWork.TaskLists.Create(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TaskListDto>(entity);
        }

        public async Task UpdateAsync(Guid ownerId, int groupId, UpdateTaskListDto updateTaskListDto)
        {
            await CheckIfGroupWithOwnerExistsAsync(ownerId, groupId);

            TaskList? entity = await _unitOfWork.TaskLists.GetByIdAndGroupIdAsync(updateTaskListDto.Id, groupId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), updateTaskListDto.Id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot update default task list");

            _mapper.Map(updateTaskListDto, entity);
            _unitOfWork.TaskLists.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid ownerId, int groupId, int id)
        {
            await CheckIfGroupWithOwnerExistsAsync(ownerId, groupId);

            TaskList? entity = await _unitOfWork.TaskLists.GetByIdAndGroupIdAsync(id, groupId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskList), id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot delete default task list");

            _unitOfWork.TaskLists.Delete(entity);
            await _unitOfWork.SaveAsync();
        }


        private async Task CheckIfGroupWithOwnerExistsAsync(Guid ownerId, int groupId)
        {
            bool isOwnerOfGroup = await _unitOfWork.TaskListGroups.GetByIdAndOwnerIdAsync(groupId, ownerId) is not null;

            if (!isOwnerOfGroup)
                throw new NotFoundException(nameof(TaskListGroup), groupId);
        }
    }
}
