using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsPlacement.Core;

namespace MoonPioneerClone.ItemsPlacement.Collections
{
    public sealed class StaticPlacementItemsCollection : IPlacementItemsCollection
    {
        private PlacementItem[] _items;
        public int FirstNullIndex => Array.IndexOf(_items, null);
        public int Count => _items.Count(i => i != null);


        public IEnumerable<PlacementItem> Items => (IEnumerable<PlacementItem>)_items.Clone();


        public StaticPlacementItemsCollection(int size)
        {
            _items = new PlacementItem[size];
        }
        

        public void Add(PlacementItem item)
        {
            _items[FirstNullIndex] = item;
        }


        public void Remove(PlacementItem item)
        {
            int itemIndex = Array.IndexOf(_items, item);
            _items[itemIndex] = null;
        }


        public void Resize(int newSize)
        {
            Array.Resize(ref _items, newSize);
        }
    }
}