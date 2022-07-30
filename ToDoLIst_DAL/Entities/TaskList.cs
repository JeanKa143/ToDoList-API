using System.ComponentModel.DataAnnotations;

namespace ToDoLIst_DAL.Entities
{
    public class TaskList
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public TaskListGroup? Group { get; set; }

        public HashSet<TaskItem>? TaskItems { get; set; }
    }
}
