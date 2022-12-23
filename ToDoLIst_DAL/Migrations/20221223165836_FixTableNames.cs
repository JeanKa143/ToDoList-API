using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoLIst_DAL.Migrations
{
    public partial class FixTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_OwnerId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksItems_TasksLists_TaskListId",
                table: "TasksItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksLists_Groups_GroupId",
                table: "TasksLists");

            migrationBuilder.DropForeignKey(
                name: "FK_TasksSteps_TasksItems_TaskItemId",
                table: "TasksSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksSteps",
                table: "TasksSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksLists",
                table: "TasksLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasksItems",
                table: "TasksItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "TasksSteps",
                newName: "TaskSteps");

            migrationBuilder.RenameTable(
                name: "TasksLists",
                newName: "TaskLists");

            migrationBuilder.RenameTable(
                name: "TasksItems",
                newName: "TaskItems");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "TaskListGroups");

            migrationBuilder.RenameIndex(
                name: "IX_TasksSteps_TaskItemId",
                table: "TaskSteps",
                newName: "IX_TaskSteps_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TasksLists_GroupId",
                table: "TaskLists",
                newName: "IX_TaskLists_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_TasksItems_TaskListId",
                table: "TaskItems",
                newName: "IX_TaskItems_TaskListId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_OwnerId",
                table: "TaskListGroups",
                newName: "IX_TaskListGroups_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskSteps",
                table: "TaskSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskListGroups",
                table: "TaskListGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_TaskLists_TaskListId",
                table: "TaskItems",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskListGroups_AspNetUsers_OwnerId",
                table: "TaskListGroups",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_TaskListGroups_GroupId",
                table: "TaskLists",
                column: "GroupId",
                principalTable: "TaskListGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSteps_TaskItems_TaskItemId",
                table: "TaskSteps",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_TaskLists_TaskListId",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskListGroups_AspNetUsers_OwnerId",
                table: "TaskListGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_TaskListGroups_GroupId",
                table: "TaskLists");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskSteps_TaskItems_TaskItemId",
                table: "TaskSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskSteps",
                table: "TaskSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskLists",
                table: "TaskLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskListGroups",
                table: "TaskListGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems");

            migrationBuilder.RenameTable(
                name: "TaskSteps",
                newName: "TasksSteps");

            migrationBuilder.RenameTable(
                name: "TaskLists",
                newName: "TasksLists");

            migrationBuilder.RenameTable(
                name: "TaskListGroups",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "TaskItems",
                newName: "TasksItems");

            migrationBuilder.RenameIndex(
                name: "IX_TaskSteps_TaskItemId",
                table: "TasksSteps",
                newName: "IX_TasksSteps_TaskItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskLists_GroupId",
                table: "TasksLists",
                newName: "IX_TasksLists_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskListGroups_OwnerId",
                table: "Groups",
                newName: "IX_Groups_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_TaskListId",
                table: "TasksItems",
                newName: "IX_TasksItems_TaskListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksSteps",
                table: "TasksSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksLists",
                table: "TasksLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasksItems",
                table: "TasksItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_OwnerId",
                table: "Groups",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksItems_TasksLists_TaskListId",
                table: "TasksItems",
                column: "TaskListId",
                principalTable: "TasksLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksLists_Groups_GroupId",
                table: "TasksLists",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TasksSteps_TasksItems_TaskItemId",
                table: "TasksSteps",
                column: "TaskItemId",
                principalTable: "TasksItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
