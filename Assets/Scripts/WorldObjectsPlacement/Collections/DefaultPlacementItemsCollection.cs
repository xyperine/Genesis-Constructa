using System;
using System.Collections.Generic;

namespace MoonPioneerClone.WorldObjectsPlacement.Collections
{
    public sealed class DefaultPlacementItemsCollection : WorldPlacementItemsCollection<WorldPlacementItem[]>
    {
        protected override int LastNonNullIndex => Array.FindLastIndex(items, i => i != null);
        public override int FirstNullIndex => Array.IndexOf(items, null);

        public override IEnumerable<WorldPlacementItem> Items => (IEnumerable<WorldPlacementItem>)items.Clone();


        public DefaultPlacementItemsCollection(int size)
        {
            items = new WorldPlacementItem[size];
        }
        

        public override void Add(WorldPlacementItem item)
        {
            items[FirstNullIndex] = item;
        }


        public override void Remove(WorldPlacementItem item)
        {
            Remove(Array.IndexOf(items, item));
        }
        
        
        public override void Remove(int index)
        {
            items[index] = null;
        }


        public override void Resize(int newSize)
        {
            Array.Resize(ref items, newSize);
        }
    }
}