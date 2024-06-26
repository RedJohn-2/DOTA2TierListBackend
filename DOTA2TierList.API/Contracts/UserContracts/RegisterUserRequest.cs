﻿
using DOTA2TierList.API.Contracts.UserContracts;

namespace DOTA2TierList.Application.Contracts
{
    public record RegisterUserRequest(string Name, string Email, string Password, string ConfirmPassword) : IUserRequest;
}
