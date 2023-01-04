using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoLIst_DAL.Migrations
{
    public partial class AddIsDoneToTaskStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "TaskSteps",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "TaskSteps");
        }
    }
}
