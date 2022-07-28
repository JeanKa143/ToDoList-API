using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoLIst_DAL.Entities;

namespace ToDoLIst_DAL.Data.Configurations
{
    internal class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(ent => ent.DueDate).HasColumnType("date");
            builder.Property(ent => ent.AddedDate).HasDefaultValueSql("GetDate()");
        }
    }
}
