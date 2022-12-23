namespace ToDoList_BAL.Models.TaskList
{
    public class TaskListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }
}
