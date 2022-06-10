using System.Collections.Generic;

namespace ColonizationMobileGame.ItemsPlacement.Core
{
    public interface IPlacementItemsCollection
    {
        int FirstNullIndex { get; }
        int Count { get; }
        IEnumerable<PlacementItem> Items { get; }

        void Add(PlacementItem item);
        void Remove(PlacementItem item);
        void Upgrade(int newSize);
    }
}