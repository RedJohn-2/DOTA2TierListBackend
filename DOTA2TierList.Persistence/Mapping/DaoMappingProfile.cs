using AutoMapper;
using DOTA2TierList.Logic.Models;
using DOTA2TierList.Logic.Models.Enums;
using DOTA2TierList.Persistence.Entities;
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


            CreateMap<Role, RoleEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src.Type));
            CreateMap<User, UserEntity>();

            CreateMap<TierListType, TierListTypeEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src.Type));

            CreateMap<TierList, TierListEntity>();

            CreateMap<Tier, TierEntity>();

        }

    }
}
