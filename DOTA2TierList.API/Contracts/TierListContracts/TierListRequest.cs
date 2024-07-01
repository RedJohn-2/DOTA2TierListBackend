using DOTA2TierList.API.Contracts.TierContracts;
using DOTA2TierList.API.Contracts.TierListTypeContracts;

namespace DOTA2TierList.API.Contracts.TierListContracts
{
    public record TierListRequest(
        string Name,
        string? Description,
        int Type,
        List<TierRequest> Tiers
        );
}
