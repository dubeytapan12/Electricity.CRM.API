using AutoMapper;
using Electricity.CRM.API.Dtos;
using Electricity.CRM.API.Entity;

namespace Electricity.CRM.API.Profiles
{
    public class ElectricityProfile : Profile
    {
        public ElectricityProfile()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<User, ForgotPasswordUserDto>().ReverseMap();
        }
    }
}
