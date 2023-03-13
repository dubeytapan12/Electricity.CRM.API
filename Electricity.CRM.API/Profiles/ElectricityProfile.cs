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
            CreateMap<ElectricityUserCommercial,ElectricityUserDtos>().ReverseMap();
            CreateMap<ElectricityUserResidential, ElectricityUserDtos>().ReverseMap();
            CreateMap<ElectricityUserFactory, ElectricityUserDtos>().ReverseMap();
            CreateMap<ElectricityUserFlat, ElectricityUserDtos>().ReverseMap();
            CreateMap<ElectricityBiller, ElectricityBillerDtos>().ReverseMap();
        }
    }
}
