using System;
using MoonPioneerClone.ItemsPlacement.Core;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Movers
{
    [Serializable]
    public sealed class PlacingPlacementItemsMover : PlacementItemsMover
    {
        private Transform _itemsParent;


        public PlacingPlacementItemsMover(Transform itemsParent)
        {
            _itemsParent = itemsParent;
        }
        
        
        public override void MoveItem(PlacementItem item, Vector3 position)
        {
            item.transform.SetParent(_itemsParent);
            base.MoveItem(item, position);
        }
    }
}