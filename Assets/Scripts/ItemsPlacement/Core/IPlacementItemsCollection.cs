using System.Collections.Generic;

namespace GenesisConstructa.ItemsPlacement.Core
{
    public interface IPlacementItemsCollection
    {
        int FirstNullIndex { get; }
        int Count { get; }
        IReadOnlyList<PlacementItem> Items { get; }

        void Add(PlacementItem item);
        void Remove(PlacementItem item);
        void Upgrade(int newSize);
    }
}