namespace ToDoList_BAL.Models.AppUser
{
    public class UserDTO : BaseUserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
