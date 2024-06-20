using DOTA2TierList.API.Contracts.UserContracts;

namespace DOTA2TierList.API.Contracts
{
    public record CreateUserRequest(string Name, string Email, string Password, string ConfirmPassword) : IUserRequest;
}
