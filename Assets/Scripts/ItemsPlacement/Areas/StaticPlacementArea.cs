using ColonizationMobileGame.ItemsPlacement.Collections;
using ColonizationMobileGame.ItemsPlacement.Core.Area;

namespace ColonizationMobileGame.ItemsPlacement.Areas
{
    public sealed class StaticPlacementArea : PlacementArea
    {
        protected override void InitializeItemsCollection()
        {
            itemsCollection = new StaticPlacementItemsCollection(placementSettings.MaxItems);
        }
    }
}