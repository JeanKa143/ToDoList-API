using AutoMapper;
using ToDoList_BAL.Models.AppUser;
using ToDoList_BAL.Models.TaskItem;
using ToDoList_BAL.Models.TaskList;
using ToDoList_BAL.Models.TaskListGroup;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<UpdateUserDto, AppUser>();
            CreateMap<CreateUserDto, AppUser>()
                .ForMember(d => d.UserName, options => options.MapFrom(s => s.Email));


            CreateMap<TaskListGroup, TaskListGroupDto>();
            CreateMap<TaskListGroup, DetailedTaskListGroupDto>();
            CreateMap<CreateTaskListGroupDto, TaskListGroup>();
            CreateMap<UpdateTaskListGroupDto, TaskListGroup>();


            CreateMap<TaskList, TaskListDto>();
            CreateMap<TaskList, DetailedTaskListDto>();
            CreateMap<CreateTaskListDto, TaskList>();
            CreateMap<UpdateTaskListDto, TaskList>();


            CreateMap<TaskItem, TaskItemDto>();
        }
    }
}
