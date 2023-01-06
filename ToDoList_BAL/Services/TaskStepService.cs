using AutoMapper;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskStep;
using ToDoList_DAL.Contracts;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Services
{
    public class TaskStepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskStepService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskStepDto>> GetAllByTaskItemIdAsync(Guid ownerId, int groupId, int listId, int taskId)
        {
            await CheckIfTaskItemExistAsync(ownerId, groupId, listId, taskId);

            IEnumerable<TaskStep> entities = await _unitOfWork.TaskSteps.GetAllByTaskItemIdAsync(taskId);
            return _mapper.Map<IEnumerable<TaskStepDto>>(entities);
        }

        public async Task CreateOrUpdateTaskStepsAsync(Guid ownerId, int groupId, int listId, int taskId, List<CreateOrUpdateTaskStepDto> taskStepDtos)
        {
            await CheckIfTaskItemExistAsync(ownerId, groupId, listId, taskId);

            int[] dtoIds = taskStepDtos.Where(x => x.Id != 0)
                .Select(x => x.Id)
                .ToArray();

            if (dtoIds.Length != dtoIds.Distinct().Count())
                throw new BadRequestException("The list of task steps contains duplicated ids");

            if ((dtoIds.Length > 0) && (dtoIds.Length != await _unitOfWork.TaskSteps.CountByIdsAndTaskItemIdAsync(dtoIds, taskId)))
                throw new NotFoundException("One or more TaskStep were not found");

            var entities = _mapper.Map<List<TaskStep>>(taskStepDtos);
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].TaskItemId = taskId;
                entities[i].Position = i;
            }

            _unitOfWork.TaskSteps.UpdateRange(entities);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid ownerId, int groupId, int listId, int taskId, int[] ids)
        {
            await CheckIfTaskItemExistAsync(ownerId, groupId, listId, taskId);

            if (ids.Length != ids.Distinct().Count())
                throw new BadRequestException("The list of task step ids contains duplicated values");

            if (ids.Length != await _unitOfWork.TaskSteps.CountByIdsAndTaskItemIdAsync(ids, taskId))
                throw new NotFoundException("One or more TaskStep were not found");

            foreach (int id in ids)
            {
                _unitOfWork.TaskSteps.Delete(new TaskStep { Id = id });
            }

            await _unitOfWork.SaveAsync();
        }


        private async Task CheckIfTaskItemExistAsync(Guid ownerId, int groupId, int listId, int taskId)
        {
            if (!await _unitOfWork.TaskListGroups.IsAnyWithOwnerIdAndGroupIdAsync(ownerId, groupId))
                throw new NotFoundException(nameof(TaskListGroup), groupId);

            if (!await _unitOfWork.TaskLists.IsAnyWithGroupIdAndListIdAsync(groupId, listId))
                throw new NotFoundException(nameof(TaskList), listId);

            if (!await _unitOfWork.TaskItems.IsAnyWithListIdAndTaskItemIdAsync(listId, taskId))
                throw new NotFoundException(nameof(TaskItem), taskId);
        }
    }
}
