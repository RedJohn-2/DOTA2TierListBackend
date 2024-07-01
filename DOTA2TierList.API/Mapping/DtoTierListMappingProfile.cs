using AutoMapper;
using DOTA2TierList.API.Contracts.TierContracts;
using DOTA2TierList.API.Contracts.TierItemContracts;
using DOTA2TierList.API.Contracts.TierListContracts;
using DOTA2TierList.API.Contracts.TierListTypeContracts;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using DOTA2TierList.Logic.Models.TierItemTypes;

namespace DOTA2TierList.API.Mapping
{
    public class DtoTierListMappingProfile : Profile
    {
        public DtoTierListMappingProfile()
        {
            CreateMap<TierItem, TierItemResponse>();
            CreateMap<Tier, TierResponse>();
            CreateMap<TierListType, TierListTypeResponse>();
            CreateMap<TierList, TierListResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.Name));

            CreateMap<TierItemRequest, TierItem>()
                .ForMember(dest => dest, opt =>
                opt.MapFrom(src => TierItemFactory.CreateTierItem((TierListTypeEnum)src.Type, src.Id)));

            CreateMap<TierRequest, Tier>();

            CreateMap<TierListRequest, TierList>()
                .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => new TierListType { Type = (TierListTypeEnum)src.Type }));

        }
    }
}
