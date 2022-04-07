using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Core
{
    public abstract class PlacementItemsMover
    {
        public virtual void MoveItem(PlacementItem item, Vector3 position)
        {
            item.MoveToArea(position);
        }
    }
}