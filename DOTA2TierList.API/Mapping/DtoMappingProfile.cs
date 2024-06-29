using AutoMapper;
using DOTA2TierList.API.Contracts;
using DOTA2TierList.API.Contracts.TierContracts;
using DOTA2TierList.API.Contracts.TierItemContracts;
using DOTA2TierList.API.Contracts.TierListContracts;
using DOTA2TierList.API.Contracts.TierListTypeContracts;
using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.Application.Contracts;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;

namespace DOTA2TierList.API.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile() 
        {
            CreateMap<RegisterUserRequest, User>();
            CreateMap<LoginUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();

            CreateMap<User, UserResponse>();


            CreateMap<TierItem, TierItemResponse>();
            CreateMap<Tier, TierResponse>();
            CreateMap<TierListType, TierListTypeResponse>();
            CreateMap<TierList, TierListResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.Name));

            CreateMap<TierItemRequest, TierItem>();
            CreateMap<TierRequest, Tier>();
            CreateMap<TierListTypeRequest, TierListType>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (TierListTypeEnum)src.Type));
            CreateMap<TierListRequest, TierList>();

        }
    }
}
