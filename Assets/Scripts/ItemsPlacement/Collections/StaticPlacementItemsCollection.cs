using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsPlacement.Core;
using UnityEngine;

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
            if (FirstNullIndex == -1)
            {
                Debug.LogError("Adding to placement item to a full collection!");
                return;
            }
            
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