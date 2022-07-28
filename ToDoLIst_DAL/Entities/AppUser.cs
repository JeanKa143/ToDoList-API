using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToDoLIst_DAL.Entities
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string? FirstName { get; set; }
        [StringLength(100)]
        public string? LastName { get; set; }

        public HashSet<TaskListGroup>? TaskListGroups { get; set; }
    }
}
