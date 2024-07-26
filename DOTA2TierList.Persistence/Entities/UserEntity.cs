using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpires { get; set; }

        public long? SteamProfileId { get; set; }

        public SteamProfileEntity? SteamProfile { get; set; }

        public List<RoleEntity>? Roles { get; set; }

        public List<TierListEntity>? TierLists { get; set; }

    }
}
