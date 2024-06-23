using DOTA2TierList.Logic.Models;

namespace DOTA2TierList.API.Contracts.UserContracts
{
    public record UpdateUserRequest(
        string Name,
        string Email
        ) : IUserRequest;
}
