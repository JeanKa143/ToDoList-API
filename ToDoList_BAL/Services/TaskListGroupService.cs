using AutoMapper;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskListGroup;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Services
{
    public class TaskListGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TaskListGroupService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<TaskListGroupDto> GetByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            TaskListGroup? entity = await _unitOfWork.TaskListGroups.GetByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), id);

            return _mapper.Map<TaskListGroupDto>(entity);
        }

        public async Task<DetailedTaskListGroupDto> GetWithDetailsByIdAndOwnerIdAsync(int id, Guid ownerId)
        {
            TaskListGroup? entity = await _unitOfWork.TaskListGroups.GetWithDetailsByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), id);

            return _mapper.Map<DetailedTaskListGroupDto>(entity);
        }

        public async Task<IEnumerable<TaskListGroupDto>> GetAllByOwnerIdAsync(Guid ownerId)
        {
            await CheckIfUserExists(ownerId);

            IEnumerable<TaskListGroup> entities = await _unitOfWork.TaskListGroups.GetAllByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<TaskListGroupDto>>(entities);
        }

        public async Task<IEnumerable<DetailedTaskListGroupDto>> GetAllWithDetailsByOwnerIdAsync(Guid ownerId)
        {
            await CheckIfUserExists(ownerId);

            IEnumerable<TaskListGroup> entities = await _unitOfWork.TaskListGroups.GetAllWithDetailsByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<DetailedTaskListGroupDto>>(entities);
        }

        public async Task<TaskListGroupDto> CreateAsync(CreateTaskListGroupDto createTaskListGroupDto)
        {
            await CheckIfUserExists(createTaskListGroupDto.OwnerId);

            TaskListGroup entity = _mapper.Map<TaskListGroup>(createTaskListGroupDto);
            _unitOfWork.TaskListGroups.Create(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TaskListGroupDto>(entity);
        }

        public async Task UpdateAsync(UpdateTaskListGroupDto updateTaskListGroupDto)
        {
            TaskListGroup? entity = await _unitOfWork.TaskListGroups.GetByIdAndOwnerIdAsync(updateTaskListGroupDto.Id, updateTaskListGroupDto.OwnerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), updateTaskListGroupDto.Id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot update default task list group");

            _mapper.Map(updateTaskListGroupDto, entity);
            _unitOfWork.TaskListGroups.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id, Guid ownerId)
        {
            TaskListGroup? entity = await _unitOfWork.TaskListGroups.GetByIdAndOwnerIdAsync(id, ownerId);

            if (entity is null)
                throw new NotFoundException(nameof(TaskListGroup), id);

            if (entity.IsDefault)
                throw new BadRequestException("Cannot delete default task list group");

            _unitOfWork.TaskListGroups.Delete(entity);
            await _unitOfWork.SaveAsync();
        }


        private async Task CheckIfUserExists(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId.ToString());

            if (user is null)
                throw new NotFoundException("User", userId);
        }
    }
}
