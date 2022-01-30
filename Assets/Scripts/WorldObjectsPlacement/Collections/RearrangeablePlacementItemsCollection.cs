using System.Collections.Generic;

namespace MoonPioneerClone.WorldObjectsPlacement.Collections
{
    public sealed class RearrangeablePlacementItemsCollection : WorldPlacementItemsCollection<List<WorldPlacementItem>>
    {
        protected override int LastNonNullIndex => items.FindLastIndex(i => i != null);
        public override int FirstNullIndex => LastNonNullIndex + 1;

        public override IEnumerable<WorldPlacementItem> Items => new List<WorldPlacementItem>(items);


        public RearrangeablePlacementItemsCollection(int count)
        {
            items = new List<WorldPlacementItem>(count);
        }


        public override void Add(WorldPlacementItem item)
        {
            items.Add(item);
        }


        public override void Remove(WorldPlacementItem item)
        {
            items.Remove(item);
        }


        public override void Remove(int index)
        {
            items.RemoveAt(index);
        }


        public override void Resize(int newSize)
        {
            items.Capacity = newSize;
        }
    }
}