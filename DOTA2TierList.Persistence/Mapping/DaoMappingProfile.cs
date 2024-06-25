using AutoMapper;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using DOTA2TierList.Logic.Models.TierItemTypes;
using DOTA2TierList.Persistence.Entities;
using DOTA2TierList.Persistence.Entities.TierItemTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Mapping
{
    public class DaoMappingProfile : Profile
    {
        public DaoMappingProfile() 
        {
            CreateMap<RoleEntity, Role>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (RoleEnum)src.Id));

            CreateMap<UserEntity, User>();

            CreateMap<TierListTypeEntity, TierListType>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (TierListTypeEnum)src.Id));

            CreateMap<TierListEntity, TierList>();

            CreateMap<TierEntity, Tier>();

            CreateMap<HeroEntity, Hero>();
            CreateMap<ArtifactEntity, Artifact>();

            CreateMap<TierItemEntity, TierItem>()
                .Include<HeroEntity, Hero>()
                .Include<ArtifactEntity, Artifact>();


            CreateMap<Role, RoleEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src.Type));

            CreateMap<User, UserEntity>();

            CreateMap<TierListType, TierListTypeEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src.Type));

            CreateMap<TierList, TierListEntity>();

            CreateMap<Tier, TierEntity>();

            CreateMap<Hero, HeroEntity>();
            CreateMap<Artifact, ArtifactEntity>();

            CreateMap<TierItem, TierItemEntity>()
                .Include<Hero, HeroEntity>()
                .Include<Artifact, ArtifactEntity>();

        }

    }
}
