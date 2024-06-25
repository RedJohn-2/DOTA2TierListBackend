using AutoMapper;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.Application.Contracts;
using DOTA2TierList.Logic.Models;

namespace DOTA2TierList.API.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile() 
        {
            CreateMap<RegisterUserRequest, User>();
            CreateMap<LoginUserRequest, User>();

            CreateMap<User, UserResponse>();

        }
    }
}
