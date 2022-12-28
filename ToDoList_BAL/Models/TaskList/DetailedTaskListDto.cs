using ToDoList_BAL.Models.TaskItem;

namespace ToDoList_BAL.Models.TaskList
{
    public class DetailedTaskListDto : TaskListDto
    {
        public HashSet<TaskItemDto>? TaskItems { get; set; }
    }
}
