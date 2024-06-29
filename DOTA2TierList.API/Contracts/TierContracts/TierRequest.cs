using DOTA2TierList.API.Contracts.TierItemContracts;

namespace DOTA2TierList.API.Contracts.TierContracts
{
    public record TierRequest(
        string Name,
        string Description,
        List<TierItemRequest> Items
        );
}
