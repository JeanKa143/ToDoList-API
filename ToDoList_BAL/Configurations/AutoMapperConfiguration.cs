using AutoMapper;
using ToDoList_BAL.Models;
using ToDoList_BAL.Models.AppUser;
using ToDoLIst_DAL.Auth;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AuthData, AuthDTO>();
            CreateMap<AppUser, UserDTO>();

            CreateMap<CreateUserDTO, AppUser>();
            CreateMap<UpdateUserDTO, AppUser>();
        }
    }
}
