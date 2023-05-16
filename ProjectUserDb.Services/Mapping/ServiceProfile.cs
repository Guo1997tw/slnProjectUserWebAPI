using AutoMapper;
using ProjectUser.Repository.Models;
using ProjectUser.Services.Dto;

namespace ProjectUser.Services.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<UserDto, UserModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(d => d.UserMobilePhone, o => o.MapFrom(s => s.UserMobilePhone));
        }
    }
}