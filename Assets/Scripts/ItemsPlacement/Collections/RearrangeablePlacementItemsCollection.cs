using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;

namespace ColonizationMobileGame.ItemsPlacement.Collections
{
    public sealed class RearrangeablePlacementItemsCollection : IPlacementItemsCollection
    {
        private readonly List<PlacementItem> _items;

        private int LastNonNullIndex => _items.FindLastIndex(i => i != null);
        public int FirstNullIndex => LastNonNullIndex + 1;
        public int Count => _items.Count(i => i != null);

        public IEnumerable<PlacementItem> Items => new List<PlacementItem>(_items);


        public RearrangeablePlacementItemsCollection(int count)
        {
            _items = new List<PlacementItem>(count);
        }


        public void Add(PlacementItem item)
        {
            _items.Add(item);
        }


        public void Remove(PlacementItem item)
        {
            _items.Remove(item);
        }

        
        public void Upgrade(int newSize)
        {
            _items.Capacity = newSize;
        }
    }
}