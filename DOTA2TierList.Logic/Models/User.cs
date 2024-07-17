using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Logic.Models
{
    
    public class User
    {

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;    

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string? RefreshToken {  get; set; }

        public DateTime? RefreshTokenExpires {  get; set; }

        public List<TierList> TierLists { get; set; } = new();

        public List<Role> Roles { get; set; } = new();

    }
}
