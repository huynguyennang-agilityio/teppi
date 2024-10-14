using AutoMapper;
using Teppi.Share.DTOs.Responses;
using Teppi.Share.Entities;

namespace Teppi.Application.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Role, opt => opt.Ignore()); // Ignore if you don't have this f
    }
}
