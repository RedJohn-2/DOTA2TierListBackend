using DOTA2TierList.API.Contracts.TierItemContracts;

namespace DOTA2TierList.API.Contracts.TierContracts
{
    public record TierResponse(
        string Name, 
        string Description,
        List<TierItemResponse> Items
        );
}
