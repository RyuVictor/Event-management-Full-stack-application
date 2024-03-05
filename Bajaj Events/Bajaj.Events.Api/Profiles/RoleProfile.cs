using AutoMapper;
using Bajaj.Events.Api.DTOs.RoleDtos;
using Bajaj.Events.Models;
namespace Bajaj.Events.Api.Profiles

{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<Role, RoleDetailsDto>();
            CreateMap<NewRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}