using DOTA2TierList.API.Contracts.TierContracts;
using DOTA2TierList.API.Contracts.TierListTypeContracts;

namespace DOTA2TierList.API.Contracts.TierListContracts
{
    public record TierListResponse(
        string Name,
        string? Description,
        TierListTypeResponse Type,
        DateTime ModifiedDate,
        string Author,
        List<TierResponse> Tiers
        );
}
