using System.Collections.Generic;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public interface IWorldPlacementItemsCollection
    {
        int FirstNullIndex { get; }
        int Count { get; }
        IEnumerable<WorldPlacementItem> Items { get; }

        void Add(WorldPlacementItem item);
        void Remove(WorldPlacementItem item);
        void Remove(int index);
        void Remove();
        void Resize(int newSize);
        WorldPlacementItem Peek();
    }
}