using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Controllers.V1;
using ToDoList_API.Errors;
using ToDoList_API.Tests.Mocks;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskList;
using ToDoList_BAL.Services;

namespace ToDoList_API.Tests.ControllerTests
{
    public class TaskListControllerTest
    {
        private readonly TaskListController _controller;

        public TaskListControllerTest() => _controller = new TaskListController(GetTaskListService());

        [Fact]
        public async void GivenAValidData_WhenGetting_ThenTaskListDtoReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;

            var result = (await _controller.Get(ownerId, groupId, listId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<TaskListDto>(result.Value);

            var data = result.Value as TaskListDto;
            Assert.NotNull(data);
            Assert.Equal(listId, data.Id);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(incorrectOwnerId, groupId, listId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(ownerId, incorrectGroupId, listId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(ownerId, groupId, incorrectListId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenGettingWithDetails_ThenDetailedTaskListDtoReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;

            var result = (await _controller.GetWithDetails(ownerId, groupId, listId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<DetailedTaskListDto>(result.Value);

            var data = result.Value as DetailedTaskListDto;
            Assert.NotNull(data);
            Assert.Equal(listId, data.Id);
            Assert.Equal(3, data.TaskItems.Count);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(incorrectOwnerId, groupId, listId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(ownerId, incorrectGroupId, listId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(ownerId, groupId, incorrectListId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenGettingAllByGroup_ThenAllTaskListDtoReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;

            var result = (await _controller.GetAllByGroup(ownerId, groupId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<TaskListDto>>(result.Value);

            var data = result.Value as IEnumerable<TaskListDto>;
            Assert.NotNull(data);
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGettingAllByGroup_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByGroup(incorrectOwnerId, groupId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenGettingAllByGroup_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByGroup(ownerId, incorrectGroupId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenCreating_ThenCreatedAtActionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var dto = new CreateTaskListDto
            {
                GroupId = groupId,
                Name = "Test task list"
            };

            var result = (await _controller.Create(ownerId, groupId, dto)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.IsAssignableFrom<TaskListDto>(result.Value);

            var data = result.Value as TaskListDto;
            Assert.NotNull(data);
            Assert.Equal(dto.Name, data.Name);
        }

        [Fact]
        public async void GivenADifferentGroupId_WhenCreating_ThenBadRequestReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId1 = 1;
            var groupId2 = 9;
            var dto = new CreateTaskListDto
            {
                GroupId = groupId2,
                Name = "Test task list"
            };

            var result = (await _controller.Create(ownerId, groupId1, dto)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The group id of the DTO cannot be different from the group id of the route", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenCreating_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var dto = new CreateTaskListDto
            {
                GroupId = groupId,
                Name = "Test task list"
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Create(incorrectOwnerId, groupId, dto));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectGroup_WhenCreating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 99;
            var dto = new CreateTaskListDto
            {
                GroupId = groupId,
                Name = "Test task list"
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Create(ownerId, groupId, dto));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenUpdating_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var taskListId = 2;
            var dto = new UpdateTaskListDto
            {
                Id = taskListId,
                GroupId = groupId,
                Name = "Update name"
            };

            var result = await _controller.Update(ownerId, groupId, dto) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenADifferentGroupId_WhenUpdating_ThenBadRequestReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId1 = 1;
            var groupId2 = 9;
            var dto = new UpdateTaskListDto
            {
                Id = 2,
                GroupId = groupId2,
                Name = "Update name"
            };

            var result = await _controller.Update(ownerId, groupId1, dto) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The group id of the DTO cannot be different from the group id of the route", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 2;
            var dto = new UpdateTaskListDto
            {
                Id = listId,
                GroupId = groupId,
                Name = "Update name"
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(incorrectOwnerId, groupId, dto));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectGroup_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 999;
            var listId = 2;
            var dto = new UpdateTaskListDto
            {
                Id = listId,
                GroupId = incorrectGroupId,
                Name = "Update name"
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(ownerId, incorrectGroupId, dto));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 4;
            var dto = new UpdateTaskListDto
            {
                Id = listId,
                GroupId = groupId,
                Name = "Update name"
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(ownerId, groupId, dto));

            Assert.Equal($"TaskList with id ({listId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfADefaultTaskList_WhenUpdating_ThenBadRequestExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var dto = new UpdateTaskListDto
            {
                Id = listId,
                GroupId = groupId,
                Name = "Update name"
            };

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _controller.Update(ownerId, groupId, dto));

            Assert.Equal($"Cannot update default task list", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenDeleting_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var taskListId = 2;

            var result = await _controller.Delete(ownerId, groupId, taskListId) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 2;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(incorrectOwnerId, groupId, listId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectGroup_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 99;
            var listId = 2;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(incorrectOwnerId, groupId, listId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 4;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, groupId, listId));

            Assert.Equal($"TaskList with id ({listId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfADefaultTaskList_WhenDeleting_ThenBadRequestExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _controller.Delete(ownerId, groupId, listId));

            Assert.Equal($"Cannot delete default task list", exception.Message);
        }


        private static TaskListService GetTaskListService()
        {
            var mockIUnitOfWork = MockIUnitOfWork.GetMock();
            return new TaskListService(mockIUnitOfWork.Object, Utils.GetMapper());
        }
    }
}
