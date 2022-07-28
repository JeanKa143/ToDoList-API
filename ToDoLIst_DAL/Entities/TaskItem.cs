namespace ToDoLIst_DAL.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsImportant { get; set; }
        public bool IsInMyDay { get; set; }
        public bool IsDone { get; set; }

        public int TaskListId { get; set; }
        public TaskList? TaskList { get; set; }

        public HashSet<TaskStep>? TaskSteps { get; set; }
    }
}
