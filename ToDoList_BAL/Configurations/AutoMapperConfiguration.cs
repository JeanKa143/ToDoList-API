using AutoMapper;
using ToDoList_BAL.Models;
using ToDoLIst_DAL.Auth;

namespace ToDoList_BAL.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AuthData, AuthDTO>();
        }
    }
}
