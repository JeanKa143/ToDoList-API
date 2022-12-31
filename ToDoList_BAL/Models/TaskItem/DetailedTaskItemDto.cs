using ToDoList_BAL.Models.TaskStep;

namespace ToDoList_BAL.Models.TaskItem
{
    public class DetailedTaskItemDto : TaskItemDto
    {
        public IEnumerable<TaskStepDto>? TaskSteps { get; set; }
    }
}
