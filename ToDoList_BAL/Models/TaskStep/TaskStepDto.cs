namespace ToDoList_BAL.Models.TaskStep
{
    public class TaskStepDto : IModelDto<int>
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
