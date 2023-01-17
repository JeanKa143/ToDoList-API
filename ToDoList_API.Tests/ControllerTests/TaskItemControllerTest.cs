using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Controllers.V1;
using ToDoList_API.Errors;
using ToDoList_API.Tests.Mocks;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskItem;
using ToDoList_BAL.Services;

namespace ToDoList_API.Tests.ControllerTests
{
    public class TaskItemControllerTest
    {
        private readonly TaskItemController _controller;

        public TaskItemControllerTest() => _controller = new TaskItemController(GetTaskItemService());

        [Fact]
        public async void GivenAValidData_WhenGetting_ThenTaskItemDtoReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var result = (await _controller.Get(ownerId, groupId, listId, taskId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<TaskItemDto>(result.Value);

            var data = result.Value as TaskItemDto;
            Assert.NotNull(data);
            Assert.Equal(taskId, data.Id);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(incorrectOwnerId, groupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(ownerId, incorrectGroupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(ownerId, groupId, incorrectListId, taskId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(ownerId, groupId, listId, incorrectTaskId));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenGettingWithDetails_ThenTaskItemDtoReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var result = (await _controller.GetWithDetails(ownerId, groupId, listId, taskId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<DetailedTaskItemDto>(result.Value);

            var data = result.Value as DetailedTaskItemDto;
            Assert.NotNull(data);
            Assert.Equal(taskId, data.Id);
            Assert.Equal(3, data.TaskSteps.Count());
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(incorrectOwnerId, groupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(ownerId, incorrectGroupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(ownerId, groupId, incorrectListId, taskId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(ownerId, groupId, listId, incorrectTaskId));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenGettingAllByList_ThenAllTaskItemDtoReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;

            var result = (await _controller.GetAllByList(ownerId, groupId, listId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<TaskItemDto>>(result.Value);

            var data = result.Value as IEnumerable<TaskItemDto>;
            Assert.NotNull(data);
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGettingAllByList_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByList(incorrectOwnerId, groupId, listId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenGettingAllByList_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByList(ownerId, incorrectGroupId, listId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenGettingAllByList_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByList(ownerId, groupId, incorrectListId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenCreating_ThenCreatedAtActionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var dto = new CreateTaskItemDto
            {
                TaskListId = listId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var result = (await _controller.Create(ownerId, groupId, listId, dto)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.IsAssignableFrom<TaskItemDto>(result.Value);

            var data = result.Value as TaskItemDto;
            Assert.NotNull(data);
            Assert.Equal(dto.Description, data.Description);
        }

        [Fact]
        public async void GivenADifferentListId_WhenCreating_ThenBadRequestReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId1 = 1;
            var listId2 = 2;
            var dto = new CreateTaskItemDto
            {
                TaskListId = listId2,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var result = (await _controller.Create(ownerId, groupId, listId1, dto)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The list id of the DTO cannot be different from the list id of the route", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenCreating_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var dto = new CreateTaskItemDto
            {
                TaskListId = listId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Create(incorrectOwnerId, groupId, listId, dto));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenCreating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var dto = new CreateTaskItemDto
            {
                TaskListId = listId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Create(ownerId, incorrectGroupId, listId, dto));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenCreating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var dto = new CreateTaskItemDto
            {
                TaskListId = incorrectListId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Create(ownerId, groupId, incorrectListId, dto));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenUpdating_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var dto = new UpdateTaskItemDto
            {
                Id = 1,
                TaskListId = listId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var result = (await _controller.Update(ownerId, groupId, listId, dto)) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenADifferentListId_WhenUpdating_ThenBadRequestReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId1 = 1;
            var listId2 = 2;
            var dto = new UpdateTaskItemDto
            {
                Id = 1,
                TaskListId = listId2,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var result = (await _controller.Update(ownerId, groupId, listId1, dto)) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The list id of the DTO cannot be different from the list id of the route", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var dto = new UpdateTaskItemDto
            {
                Id = 1,
                TaskListId = listId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(incorrectOwnerId, groupId, listId, dto));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var dto = new UpdateTaskItemDto
            {
                Id = 1,
                TaskListId = listId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(ownerId, incorrectGroupId, listId, dto));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var dto = new UpdateTaskItemDto
            {
                Id = 1,
                TaskListId = incorrectListId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(ownerId, groupId, incorrectListId, dto));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectItemId = 99;
            var dto = new UpdateTaskItemDto
            {
                Id = incorrectItemId,
                TaskListId = listId,
                Description = "Test TaskItem",
                IsImportant = false,
                IsInMyDay = false
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(ownerId, groupId, listId, dto));

            Assert.Equal($"TaskItem with id ({incorrectItemId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenUpdatingMarkAsDone_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;


            var result = (await _controller.UpdateMarkAsDone(ownerId, groupId, listId, taskId)) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenUpdatingMarkAsDone_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsDone(incorrectOwnerId, groupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenUpdatingMarkAsDone_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsDone(ownerId, incorrectGroupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenUpdatingMarkAsDone_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsDone(ownerId, groupId, incorrectListId, taskId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenUpdatingMarkAsDone_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsDone(ownerId, groupId, listId, incorrectTaskId));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenUpdatingMarkAsNotDone_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;


            var result = (await _controller.UpdateMarkAsNotDone(ownerId, groupId, listId, taskId)) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenUpdatingMarkAsNotDone_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsNotDone(incorrectOwnerId, groupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenUpdatingMarkAsNotDone_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsNotDone(ownerId, incorrectGroupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenUpdatingMarkAsNotDone_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsNotDone(ownerId, groupId, incorrectListId, taskId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenUpdatingMarkAsNotDone_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateMarkAsNotDone(ownerId, groupId, listId, incorrectTaskId));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenDeleting_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;


            var result = (await _controller.Delete(ownerId, groupId, listId, taskId)) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(incorrectOwnerId, groupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, incorrectGroupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, groupId, incorrectListId, taskId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, groupId, listId, incorrectTaskId));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }


        private static TaskItemService GetTaskItemService()
        {
            var mockIUnitOfWork = MockIUnitOfWork.GetMock();
            return new TaskItemService(mockIUnitOfWork.Object, Utils.GetMapper());
        }
    }
}
