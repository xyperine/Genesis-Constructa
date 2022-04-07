using MoonPioneerClone.ItemsPlacement.Core;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Movers
{
    public sealed class DestroyingPlacementItemsMover : PlacementItemsMover
    {
        public override void MoveItem(PlacementItem item, Vector3 position)
        {
            base.MoveItem(item, position);
            item.Kill();
        }
    }
}