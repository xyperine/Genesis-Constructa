using System;
using System.Linq;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public class WorldPlacementItemsCollection
    {
        private WorldPlacementItem[] _items;

        private int LastNonNullIndex => Array.FindLastIndex(_items, i => i != null);
        public int FirstNullIndex => Array.IndexOf(_items, null);
        
        public int Count => _items.Count(i => i != null);

        public int IndexOf(WorldPlacementItem item) => Array.IndexOf(_items, item);


        public WorldPlacementItemsCollection(int size)
        {
            _items = new WorldPlacementItem[size];
        }
        

        public void Add(WorldPlacementItem item)
        {
            _items[FirstNullIndex] = item;
        }


        public void Remove(WorldPlacementItem item)
        {
            Remove(IndexOf(item));
        }
        
        
        public void Remove(int index)
        {
            _items[index] = null;
        }


        public void Remove()
        {
            Remove(LastNonNullIndex);
        }


        public WorldPlacementItem Pop()
        {
            if (LastNonNullIndex == -1)
            {
                return null;
            }
            
            WorldPlacementItem item = _items[LastNonNullIndex];
            Remove(item);

            return item;
        }


        public void Resize(int newSize)
        {
            Array.Resize(ref _items, newSize);
        }
    }
}