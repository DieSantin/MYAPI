using AutoMapper;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.Models.EXAPIEntities;

namespace MYAPI.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + src.LastName))
            .ForMember(dest => dest.Employment, opt => opt.MapFrom(src => src.Employment.Title))
            .ForMember(dest => dest.KeySkill, opt => opt.MapFrom(src => src.Employment.KeySkill))
            .ForMember(dest => dest.ProfilePicUrl, opt => opt.MapFrom(src => src.Avatar.ToString()))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Address.Id.ToString()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString()));

        CreateMap<User, AddressDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Address.Id))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.StreetName + src.Address.StreetAddress))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.Address.CreationDate.ToString()));
    }
}
