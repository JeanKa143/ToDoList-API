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
            CreateMap<AuthData, AuthDTO>().ReverseMap();
            CreateMap<AppUser, UserDTO>();

            CreateMap<CreateUserDTO, AppUser>()
                .ForMember(d => d.UserName, options => options.MapFrom(s => s.Email));
            CreateMap<UpdateUserDTO, AppUser>();
        }
    }
}
