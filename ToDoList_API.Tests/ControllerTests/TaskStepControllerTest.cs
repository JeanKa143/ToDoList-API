using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Controllers.V1;
using ToDoList_API.Errors;
using ToDoList_API.Tests.Mocks;
using ToDoList_BAL.Exceptions;
using ToDoList_BAL.Models.TaskStep;
using ToDoList_BAL.Services;

namespace ToDoList_API.Tests.ControllerTests
{
    public class TaskStepControllerTest
    {
        private readonly TaskStepController _controller;

        public TaskStepControllerTest() => _controller = new TaskStepController(GetTaskStepService());

        [Fact]
        public async void GivenAValidData_WhenGettingAllByTaskItem_ThenAllTaskStepDtoReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var result = (await _controller.GetAllByTaskItem(ownerId, groupId, listId, taskId)).Result as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<TaskStepDto>>(result.Value);

            var data = result.Value as IEnumerable<TaskStepDto>;
            Assert.NotNull(data);
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenGettingAllByTaskItem_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByTaskItem(incorrectOwnerId, groupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenGettingAllByTaskItem_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByTaskItem(ownerId, incorrectGroupId, listId, taskId));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenGettingAllByTaskItem_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByTaskItem(ownerId, groupId, incorrectListId, taskId));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenGettingAllByTaskItem_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetAllByTaskItem(ownerId, groupId, listId, incorrectTaskId));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenUpdatingOrCreating_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var dtos = new List<CreateOrUpdateTaskStepDto>
            {
                new()
                {
                    Id = 1,
                    Description = "Update step",
                    IsDone = false
                },
                new(){
                    Id = 0,
                    Description = "Create step",
                    IsDone = false
                },
            };

            var result = await _controller.UpdateOrCreateTaskSteps(ownerId, groupId, listId, taskId, dtos) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnEmptyList_WhenUpdatingOrCreating_ThenBadRequestReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var dtos = new List<CreateOrUpdateTaskStepDto>();

            var result = await _controller.UpdateOrCreateTaskSteps(ownerId, groupId, listId, taskId, dtos) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The list of task steps cannot be empty", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenUpdatingOrCreating_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var dtos = new List<CreateOrUpdateTaskStepDto>
            {
                new()
                {
                    Id = 1,
                    Description = "Update step",
                    IsDone = false
                }
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateOrCreateTaskSteps(incorrectOwnerId, groupId, listId, taskId, dtos));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenUpdatingOrCreating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;
            var dtos = new List<CreateOrUpdateTaskStepDto>
            {
                new()
                {
                    Id = 1,
                    Description = "Update step",
                    IsDone = false
                }
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateOrCreateTaskSteps(ownerId, incorrectGroupId, listId, taskId, dtos));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenUpdatingOrCreating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;
            var dtos = new List<CreateOrUpdateTaskStepDto>
            {
                new()
                {
                    Id = 1,
                    Description = "Update step",
                    IsDone = false
                }
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateOrCreateTaskSteps(ownerId, groupId, incorrectListId, taskId, dtos));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenUpdatingOrCreating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;
            var dtos = new List<CreateOrUpdateTaskStepDto>
            {
                new()
                {
                    Id = 1,
                    Description = "Update step",
                    IsDone = false
                }
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateOrCreateTaskSteps(ownerId, groupId, listId, incorrectTaskId, dtos));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenTwoOrMoreTaskStepsWithTheSameId_WhenUpdatingOrCreating_ThenBadRequestExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var dtos = new List<CreateOrUpdateTaskStepDto>
            {
                new()
                {
                    Id = 1,
                    Description = "Update step",
                    IsDone = false
                },
                new()
                {
                    Id = 1,
                    Description = "Update step",
                    IsDone = false
                }
            };

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _controller.UpdateOrCreateTaskSteps(ownerId, groupId, listId, taskId, dtos));

            Assert.Equal($"The list of task steps contains duplicated ids", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskStep_WhenUpdatingOrCreating_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var dtos = new List<CreateOrUpdateTaskStepDto>
            {
                new()
                {
                    Id = 99,
                    Description = "Update step",
                    IsDone = false
                }
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateOrCreateTaskSteps(ownerId, groupId, listId, taskId, dtos));

            Assert.Equal($"One or more TaskStep were not found", exception.Message);
        }


        [Fact]
        public async void GivenAValidData_WhenDeleting_ThenNoContentReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var taskStepIds = new int[] { 1, 2 };

            var result = await _controller.Delete(ownerId, groupId, listId, taskId, taskStepIds) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async void GivenAnEmptyArray_WhenDeleting_ThenBadRequestReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var taskStepIds = Array.Empty<int>();

            var result = await _controller.Delete(ownerId, groupId, listId, taskId, taskStepIds) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsAssignableFrom<BadRequestError>(result.Value);

            var error = result.Value as BadRequestError;
            Assert.NotNull(error);
            Assert.Equal("The list of task step ids cannot be empty", error.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectOwner_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var incorrectOwnerId = Guid.Parse("01B6685E-269E-4A6E-B44F-C64F8956AEB2");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var taskStepIds = new int[] { 1, 2 };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(incorrectOwnerId, groupId, listId, taskId, taskStepIds));

            Assert.Equal($"TaskListGroup with id ({groupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskListGroup_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var incorrectGroupId = 99;
            var listId = 1;
            var taskId = 1;
            var taskStepIds = new int[] { 1, 2 };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, incorrectGroupId, listId, taskId, taskStepIds));

            Assert.Equal($"TaskListGroup with id ({incorrectGroupId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskList_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var incorrectListId = 4;
            var taskId = 1;
            var taskStepIds = new int[] { 1, 2 };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, groupId, incorrectListId, taskId, taskStepIds));

            Assert.Equal($"TaskList with id ({incorrectListId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskItem_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var incorrectTaskId = 99;
            var taskStepIds = new int[] { 1, 2 };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, groupId, listId, incorrectTaskId, taskStepIds));

            Assert.Equal($"TaskItem with id ({incorrectTaskId}) was not found", exception.Message);
        }

        [Fact]
        public async void GivenOneOrMoreRepeatedIds_WhenDeleting_ThenBadRequestExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var taskStepIds = new int[] { 1, 1 };

            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _controller.Delete(ownerId, groupId, listId, taskId, taskStepIds));

            Assert.Equal($"The list of task step ids contains duplicated values", exception.Message);
        }

        [Fact]
        public async void GivenAnIdOfAnIncorrectTaskStep_WhenDeleting_ThenNotFoundExceptionReturns()
        {
            var ownerId = Guid.Parse("6934621e-7df1-44b4-bed6-f411b6e47487");
            var groupId = 1;
            var listId = 1;
            var taskId = 1;
            var taskStepIds = new int[] { 99 };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.Delete(ownerId, groupId, listId, taskId, taskStepIds));

            Assert.Equal($"One or more TaskStep were not found", exception.Message);
        }


        private static TaskStepService GetTaskStepService()
        {
            var mockIUnitOfWork = MockIUnitOfWork.GetMock();
            return new TaskStepService(mockIUnitOfWork.Object, Utils.GetMapper());
        }
    }
}
