using AutoMapper;
using Bajaj.Events.Models;
using Bajaj.Events.Api.DTOs.UserDtos;


namespace Bajaj.Events.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserDetailsDto>();
            CreateMap<NewUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
