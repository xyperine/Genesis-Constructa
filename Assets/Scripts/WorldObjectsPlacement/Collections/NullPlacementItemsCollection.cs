using System;
using System.Collections.Generic;

namespace MoonPioneerClone.WorldObjectsPlacement.Collections
{
    public sealed class NullPlacementItemsCollection : IWorldPlacementItemsCollection
    {
        public int FirstNullIndex => - 1;
        public int Count => 0;
        public IEnumerable<WorldPlacementItem> Items { get; } = Array.Empty<WorldPlacementItem>();


        public void Add(WorldPlacementItem item)
        {
        }


        public void Remove(WorldPlacementItem item)
        {
        }


        public void Remove(int index)
        {
        }


        public void Remove()
        {
        }


        public WorldPlacementItem Peek()
        {
            return null;
        }


        public void Resize(int newSize)
        {
        }
    }
}