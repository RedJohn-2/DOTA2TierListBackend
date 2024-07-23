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
                .ConstructUsing((src, context) =>
                {
                    var type = context.Mapper.Map<TierListTypeResponse>(src.Type);
                    var tiers = context.Mapper.Map<List<TierResponse>>(src.Tiers);
                    return new TierListResponse(src.Name, src.Description, type, src.ModifiedDate, src.User.Name, tiers);
                });

            CreateMap<TierItemRequest, TierItem>()
                .ConstructUsing(src => TierItemFactory.CreateTierItem((TierListTypeEnum)src.Type, src.Id));

            CreateMap<TierRequest, Tier>();

            CreateMap<TierListRequest, TierList>()
                .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => new TierListType { Type = (TierListTypeEnum)src.Type }));

        }
    }
}
