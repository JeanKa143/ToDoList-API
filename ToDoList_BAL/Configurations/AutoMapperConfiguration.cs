using AutoMapper;
using ToDoList_BAL.Models.AppUser;
using ToDoLIst_DAL.Entities;

namespace ToDoList_BAL.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AppUser, UserDTO>();

            CreateMap<CreateUserDTO, AppUser>()
                .ForMember(d => d.UserName, options => options.MapFrom(s => s.Email));
            CreateMap<UpdateUserDTO, AppUser>();
        }
    }
}
