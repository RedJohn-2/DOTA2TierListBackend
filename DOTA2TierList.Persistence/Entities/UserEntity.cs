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

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpire { get; set; }

        public List<RoleEntity> Roles { get; set; } = [];

        public List<TierListEntity> TierLists { get; set; } = [];

    }
}
