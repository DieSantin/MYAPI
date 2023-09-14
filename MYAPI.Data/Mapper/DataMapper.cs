using AutoMapper;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.Data.Entities;

namespace MYAPI.AutoMapper;

public class DataMapper : Profile
{
    public DataMapper()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid.ToString()))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName},{src.LastName}"))
            .ForMember(dest => dest.Employment, opt => opt.MapFrom(src => src.Employment.Title))
            .ForMember(dest => dest.KeySkill, opt => opt.MapFrom(src => src.Employment.KeySkill))
            .ForMember(dest => dest.ProfilePicUrl, opt => opt.MapFrom(src => src.Avatar.ToString()))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => $"{src.StreetName},{src.StreetAddress}"))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString()));

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom((src, dest) => src.FullName == null ? null : src.FullName.Split(",")[0]))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom((src, dest) => src.FullName == null || src.FullName.Split(",").Count() < 2 ? null : src.FullName.Split(",")[1]))
            .ForMember(dest => dest.Employment, opt => opt.MapFrom(src => new Employment
            {
                Title = src.Employment,
                KeySkill = src.KeySkill
            }))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.ProfilePicUrl != null ? new Uri(src.ProfilePicUrl) : null));

        CreateMap<AddressDto, Address>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString()));
    }
}
