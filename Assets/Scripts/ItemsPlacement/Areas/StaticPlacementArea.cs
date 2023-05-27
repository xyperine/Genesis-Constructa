using GenesisConstructa.ItemsPlacement.Collections;
using GenesisConstructa.ItemsPlacement.Core.Area;

namespace GenesisConstructa.ItemsPlacement.Areas
{
    public sealed class StaticPlacementArea : PlacementArea
    {
        protected override void InitializeItemsCollection()
        {
            itemsCollection = new StaticPlacementItemsCollection(placementSettings.MaxItems);
        }
    }
}