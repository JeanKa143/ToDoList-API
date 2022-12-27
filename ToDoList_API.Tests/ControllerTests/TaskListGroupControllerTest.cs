using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Controllers;
using ToDoList_API.Errors;
using ToDoList_API.Tests.Mocks;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskListGroup;
using ToDoList_BAL.Services;

namespace ToDoList_API.Tests.ControllerTests
{
    public class TaskListGroupControllerTest
    {
        private readonly TaskListGroupController _controller;

        public TaskListGroupControllerTest() => _controller = new TaskListGroupController(GetTaskListGroupService());

        [Fact]
        public async void GivenAnIdOfAnExistingTaskListGroup_WhenGetting_ThenTaskListGroupDtoReturns()
        {
            var taskListGroupId = 1;
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");

            var result = (await _controller.Get(ownerId, taskListGroupId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<TaskListGroupDto>(result.Value);

            var data = result.Value as TaskListGroupDto;
            Assert.NotNull(data);
            Assert.Equal(taskListGroupId, data.Id);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingTaskListGroup_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var nonExistingTaskListGroupId = 999;
            var existingOwnerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(existingOwnerId, nonExistingTaskListGroupId));

            Assert.Equal($"TaskListGroup with id ({nonExistingTaskListGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGetting_ThenNotFoundExceptionReturns()
        {
            var existingTaskListGroupId = 1;
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Get(incorrectOwnerId, existingTaskListGroupId));

            Assert.Equal($"TaskListGroup with id ({existingTaskListGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnExistingTaskListGroup_WhenGettingWithDetails_ThenDetailedTaskListGroupDtoReturns()
        {
            var taskListGroupId = 1;
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");

            var result = (await _controller.GetWithDetails(ownerId, taskListGroupId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<DetailedTaskListGroupDto>(result.Value);

            var data = result.Value as DetailedTaskListGroupDto;
            Assert.NotNull(data);
            Assert.Equal(data.Id, taskListGroupId);
            Assert.Equal(3, data.TaskLists.Count);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingTaskListGroup_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var nonExistingTaskListGroupId = 999;
            var existingOwnerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(existingOwnerId, nonExistingTaskListGroupId));

            Assert.Equal($"TaskListGroup with id ({nonExistingTaskListGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGettingWithDetails_ThenNotFoundExceptionReturns()
        {
            var existingTaskListGroupId = 1;
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetWithDetails(incorrectOwnerId, existingTaskListGroupId));

            Assert.Equal($"TaskListGroup with id ({existingTaskListGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnExistingOwner_WhenGettingAll_ThenAllTaskListGroupDtoReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");

            var result = (await _controller.GetAll(ownerId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<TaskListGroupDto>>(result.Value);

            var data = result.Value as IEnumerable<TaskListGroupDto>;
            Assert.NotNull(data);
            Assert.Equal(2, data.Count());
        }

        [Fact]
        public async void GivenAnIdOfANonExistingOwner_WhenGettingAll_ThenEmptyListReturns()
        {
            var ownerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");

            var result = (await _controller.GetAll(ownerId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<TaskListGroupDto>>(result.Value);

            var data = result.Value as IEnumerable<TaskListGroupDto>;
            Assert.NotNull(data);
            Assert.Equal(Enumerable.Empty<TaskListGroupDto>(), data);
        }

        [Fact]
        public async void GivenAnIdOfAnExistingOwner_WhenGettingAllWithDetails_ThenAllDetailedTaskListGroupDtoReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");

            var result = (await _controller.GetAllWithDetails(ownerId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<DetailedTaskListGroupDto>>(result.Value);

            var data = result.Value as IEnumerable<DetailedTaskListGroupDto>;
            Assert.NotNull(data);
            Assert.Equal(2, data.Count());
        }

        [Fact]
        public async void GivenAnIdOfANonExistingOwner_WhenGettingAllWithDetails_ThenEmptyListReturns()
        {
            var ownerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");

            var result = (await _controller.GetAllWithDetails(ownerId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<DetailedTaskListGroupDto>>(result.Value);

            var data = result.Value as IEnumerable<DetailedTaskListGroupDto>;
            Assert.NotNull(data);
            Assert.Equal(Enumerable.Empty<DetailedTaskListGroupDto>(), data);
        }

        [Fact]
        public async void GivenAValidData_WhenCreating_ThenCreatedAtActionReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var createTaskListGroupDto = new CreateTaskListGroupDto
            {
                Name = "New TaskListGroup",
                OwnerId = ownerId
            };

            var result = (await _controller.Create(ownerId, createTaskListGroupDto)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.IsAssignableFrom<TaskListGroupDto>(result.Value);

            var data = result.Value as TaskListGroupDto;
            Assert.NotNull(data);
            Assert.Equal(createTaskListGroupDto.Name, data.Name);
        }

        [Fact]
        public async void GivenADifferentOwnerId_WhenCreating_ThenBadRequestReturns()
        {
            var ownerId1 = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var ownerId2 = Guid.Parse("aa8a8a74-8c60-4508-a440-ea492abf6a55");
            var createTaskListGroupDto = new CreateTaskListGroupDto
            {
                Name = "New TaskListGroup",
                OwnerId = ownerId1
            };

            var result = (await _controller.Create(ownerId2, createTaskListGroupDto)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The owner id of the DTO cannot be different from the user id of the route", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnExistingTaskListGroup_WhenUpdating_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var updateTaskListGroupDto = new UpdateTaskListGroupDto
            {
                Id = 2,
                OwnerId = ownerId,
                Name = "Update TaskListGroup"
            };

            var result = await _controller.Update(ownerId, updateTaskListGroupDto) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfADefaultTaskListGroup_WhenUpdating_ThenBadRequestExceptionReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var updateTaskListGroupDto = new UpdateTaskListGroupDto
            {
                Id = 1,
                OwnerId = ownerId,
                Name = "Update TaskListGroup"
            };

            var exception =
                await Assert.ThrowsAsync<BadRequestException>(async () => await _controller.Update(ownerId, updateTaskListGroupDto));

            Assert.Equal("Cannot update default task list group", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingTaskListGroup_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var nonExistingTaskListGroupId = 99;
            var updateTaskListGroupDto = new UpdateTaskListGroupDto
            {
                Id = nonExistingTaskListGroupId,
                OwnerId = ownerId,
                Name = "Update TaskListGroup"
            };

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(ownerId, updateTaskListGroupDto));

            Assert.Equal($"TaskListGroup with id ({nonExistingTaskListGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenUpdating_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("6641cc94-d497-4b2b-9989-760f5d0ee647");
            var existingTaskListGroupId = 1;
            var updateTaskListGroupDto = new UpdateTaskListGroupDto
            {
                Id = existingTaskListGroupId,
                OwnerId = incorrectOwnerId,
                Name = "Update TaskListGroup"
            };

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Update(incorrectOwnerId, updateTaskListGroupDto));

            Assert.Equal($"TaskListGroup with id ({existingTaskListGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenADifferentOwnerId_WhenUpdating_ThenBadRequestReturns()
        {
            var ownerId1 = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var ownerId2 = Guid.Parse("aa8a8a74-8c60-4508-a440-ea492abf6a55");
            var updateTaskListGroupDto = new UpdateTaskListGroupDto
            {
                Id = 2,
                OwnerId = ownerId1,
                Name = "Update TaskListGroup"
            };

            var result = await _controller.Update(ownerId2, updateTaskListGroupDto) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The owner id of the DTO cannot be different from the user id of the route", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnExistingTaskListGroup_WhenDeleting_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var taskListGroupId = 2;

            var result = await _controller.Delete(ownerId, taskListGroupId) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnIdOfADefaultTaskListGroup_WhenDeleting_ThenBadRequestExceptionReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var taskListGroupId = 1;

            var exception =
                await Assert.ThrowsAsync<BadRequestException>(async () => await _controller.Delete(ownerId, taskListGroupId));

            Assert.Equal("Cannot delete default task list group", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingTaskListGroup_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("b171ba77-2247-4112-9aeb-fd171a433b23");
            var nonExistingTaskListGroupId = 99;

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, nonExistingTaskListGroupId));

            Assert.Equal($"TaskListGroup with id ({nonExistingTaskListGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("6641cc94-d497-4b2b-9989-760f5d0ee647");
            var taskListGroupId = 1;

            var exception =
                await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(incorrectOwnerId, taskListGroupId));

            Assert.Equal($"TaskListGroup with id ({taskListGroupId}) was not found", exception.Message);
        }


        private static TaskListGroupService GetTaskListGroupService()
        {
            var mock = MockITaskListGroupRepository.GetMock();
            return new TaskListGroupService(mock.Object, Utils.GetMapper());
        }
    }
}
