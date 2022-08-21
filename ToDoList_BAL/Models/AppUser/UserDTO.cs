namespace ToDoList_BAL.Models.AppUser
{
    public class UserDto : BaseUserDto, IModelDto<Guid>
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
