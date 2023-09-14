using AutoMapper;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.ExApi.Models;

namespace MYAPI.AutoMapper;

public class ExApiMapper : Profile
{
    public ExApiMapper()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + src.LastName))
            .ForMember(dest => dest.Employment, opt => opt.MapFrom(src => src.Employment.Title))
            .ForMember(dest => dest.KeySkill, opt => opt.MapFrom(src => src.Employment.KeySkill))
            .ForMember(dest => dest.ProfilePicUrl, opt => opt.MapFrom(src => src.Avatar.ToString()))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.StreetName + src.StreetAddress))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString()));
    }
}
