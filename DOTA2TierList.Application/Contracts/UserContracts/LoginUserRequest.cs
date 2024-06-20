
using DOTA2TierList.Application.Contracts.UserContracts;

namespace DOTA2TierList.Application.Contracts
{
    public record LoginUserRequest(string Email, string Password) : IUserRequest;
}
