using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoLIst_DAL.Migrations
{
    public partial class AddIsDefaultPropToTaskListAndTaskListGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "TasksLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "TasksLists");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Groups");
        }
    }
}
