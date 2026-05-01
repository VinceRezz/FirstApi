
using AutoMapper;
using FirstApi.DTOs;
using FirstApi.Models;

namespace FirstApi.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreateUserDto, User>();
        }
    }
}

