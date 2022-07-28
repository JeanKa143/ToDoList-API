using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<TaskListGroup> Groups => Set<TaskListGroup>();
        public DbSet<TaskList> TasksLists => Set<TaskList>();
        public DbSet<TaskItem> TasksItems => Set<TaskItem>();
        public DbSet<TaskStep> TasksSteps => Set<TaskStep>();
    }
}
