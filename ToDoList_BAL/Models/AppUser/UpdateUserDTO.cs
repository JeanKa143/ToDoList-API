namespace ToDoList_BAL.Models.AppUser
{
    public class UpdateUserDTO : BaseUserDTO, IModelDTO<Guid>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}
