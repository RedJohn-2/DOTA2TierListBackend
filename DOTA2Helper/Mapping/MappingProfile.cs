using AutoMapper;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.Logic.Models;

namespace DOTA2TierList.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            this.CreateMap<User, UserResponse>();
        }
    }
}
