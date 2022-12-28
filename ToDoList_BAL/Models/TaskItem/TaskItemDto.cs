namespace ToDoList_BAL.Models.TaskItem
{
    public class TaskItemDto : IModelDto<int>
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsImportant { get; set; }
        public bool IsInMyDay { get; set; }
        public bool IsDone { get; set; }
    }
}