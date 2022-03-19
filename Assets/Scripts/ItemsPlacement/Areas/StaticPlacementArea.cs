using MoonPioneerClone.ItemsPlacement.Collections;
using MoonPioneerClone.ItemsPlacement.Core.Area;

namespace MoonPioneerClone.ItemsPlacement.Areas
{
    public sealed class StaticPlacementArea : PlacementArea
    {
        protected override void InitializeItemsCollection()
        {
            itemsCollection = new StaticPlacementItemsCollection(placementSettings.MaxItems);
        }
    }
}