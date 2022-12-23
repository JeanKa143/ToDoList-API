using ToDoList_BAL.Models.TaskList;

namespace ToDoList_BAL.Models.TaskListGroup
{
    public class DetailedTaskListGroupDto : TaskListGroupDto
    {
        public HashSet<TaskListDto>? TaskLists { get; set; }
    }
}
