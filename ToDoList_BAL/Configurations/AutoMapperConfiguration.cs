using AutoMapper;
using ToDoList_BAL.Models.AppUser;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AppUser, UserDto>();

            CreateMap<CreateUserDto, AppUser>()
                .ForMember(d => d.UserName, options => options.MapFrom(s => s.Email));
            CreateMap<UpdateUserDto, AppUser>();
        }
    }
}
