namespace ToDoList_BAL.Models.AppUser
{
    public class UpdateUserDTO : BaseUserDTO, IModel<Guid>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}
