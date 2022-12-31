namespace ToDoList_BAL.Models.TaskItem
{
    public class TaskItemDto : BaseTaskItemDto, IModelDto<int>
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
    }
}