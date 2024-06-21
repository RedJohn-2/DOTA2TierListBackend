using AutoMapper;
using DOTA2TierList.Application.Contracts;
using DOTA2TierList.Logic.Models;

namespace DOTA2TierList.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<RegisterUserRequest, User>()
                .AfterMap((_, dest, opt) =>
                {
                    if (opt.Items.ContainsKey("PasswordHash"))
                        dest.PasswordHash = (opt.Items["PasswordHash"] as string)!;
                });

            this.CreateMap<User, UserResponse>();
        }
    }
}
