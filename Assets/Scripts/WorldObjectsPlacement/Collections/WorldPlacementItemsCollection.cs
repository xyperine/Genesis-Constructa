using System.Collections.Generic;
using System.Linq;

namespace MoonPioneerClone.WorldObjectsPlacement.Collections
{
    public abstract class WorldPlacementItemsCollection<TCollection> : IWorldPlacementItemsCollection
        where TCollection : IList<WorldPlacementItem>
    {
        protected TCollection items;

        protected abstract int LastNonNullIndex { get; }
        public abstract int FirstNullIndex { get; }
        public int Count => items.Count(i => i != null);

        public abstract IEnumerable<WorldPlacementItem> Items { get; }


        public abstract void Add(WorldPlacementItem item);
        public abstract void Remove(WorldPlacementItem item);
        public abstract void Remove(int index);


        public virtual void Remove()
        {
            Remove(LastNonNullIndex);
        }


        public abstract void Resize(int newSize);


        public virtual WorldPlacementItem Peek()
        {
            return LastNonNullIndex == -1 ? null : items[LastNonNullIndex];
        }
    }
}