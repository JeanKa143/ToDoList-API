using System.ComponentModel.DataAnnotations;

namespace ToDoList_BAL.Models.Auth
{
    public class AuthDto : IModelDto<Guid>
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
