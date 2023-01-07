using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoLIst_DAL.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "e45ad016-aaa4-4dd3-a5ee-69dcb07604c5",
                    ConcurrencyStamp = "4a98c78f-584c-4914-895a-41957d8906db",
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}
