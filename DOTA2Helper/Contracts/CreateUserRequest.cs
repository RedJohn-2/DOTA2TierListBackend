namespace DOTA2TierList.API.Contracts
{
    public record CreateUserRequest(string Name, string Email, string PasswordHash);
}
