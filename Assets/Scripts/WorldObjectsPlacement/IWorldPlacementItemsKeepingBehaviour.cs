using System.Collections.Generic;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public interface IWorldPlacementItemsKeepingBehaviour
    {
        public IEnumerable<WorldPlacementItem> Items { get; }
        public int FirstNullIndex { get; }
        public int Count { get; }

        public WorldPlacementItem TryPeek();
        public void TryAdd(WorldPlacementItem item);
        public void TryRemove(WorldPlacementItem item);
    }
}