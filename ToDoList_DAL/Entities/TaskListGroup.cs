using System.ComponentModel.DataAnnotations;

namespace ToDoLIst_DAL.Entities
{
    public class TaskListGroup
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        public bool IsDefault { get; set; }

        public string OwnerId { get; set; } = string.Empty;
        public AppUser? Owner { get; set; }

        public HashSet<TaskList>? TaskLists { get; set; }
    }
}
