namespace ToDoList_BAL.Models.AppUser
{
    public class UpdateUserDto : BaseUserDto, IModelDto<Guid>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}
