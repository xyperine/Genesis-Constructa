using System.Collections.Generic;

namespace MoonPioneerClone.WorldObjectsPlacement.Grid
{
    public class GridItemsKeepingBehaviour : IWorldPlacementItemsKeepingBehaviour
    {
        private readonly WorldPlacementItemsCollection _items;

        public IEnumerable<WorldPlacementItem> Items => _items.Items;

        public int Count => _items.Count;
        public int FirstNullIndex => _items.FirstNullIndex;


        public GridItemsKeepingBehaviour(int count)
        {
            _items = new WorldPlacementItemsCollection(count);
        }
        
        
        public WorldPlacementItem TryPeek()
        {
            return _items.Peek();
        }


        public void TryAdd(WorldPlacementItem item)
        {
            _items.Add(item);
        }


        public void TryRemove(WorldPlacementItem item)
        {
            _items.Remove(item);
        }
    }
}