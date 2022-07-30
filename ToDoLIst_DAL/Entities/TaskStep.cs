namespace ToDoLIst_DAL.Entities
{
    public class TaskStep
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Position { get; set; }

        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
    }
}
