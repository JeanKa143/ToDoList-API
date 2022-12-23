namespace ToDoList_BAL.Models.TaskListGroup
{
    public class TaskListGroupDto : IModelDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }
}
