using System;
using System.Collections.Generic;

namespace MoonPioneerClone.WorldObjectsPlacement.OnePoint
{
    public class OnePointItemsKeepingBehaviour : IWorldPlacementItemsKeepingBehaviour
    {
        public int FirstNullIndex => -1;
        public int Count => 0;
        
        public IEnumerable<WorldPlacementItem> Items => Array.Empty<WorldPlacementItem>();
        
        
        public void TryAdd(WorldPlacementItem item)
        {
            return;
        }


        public void TryRemove(WorldPlacementItem item)
        {
            return;
        }


        public WorldPlacementItem TryPeek()
        {
            return null;
        }
    }
}