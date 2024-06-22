
using DOTA2TierList.API.Contracts.UserContracts;

namespace DOTA2TierList.Application.Contracts
{
    public record LoginUserRequest(string Email, string Password) : IUserRequest;
}
