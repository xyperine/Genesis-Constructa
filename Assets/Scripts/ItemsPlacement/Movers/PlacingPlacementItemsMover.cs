using System;
using MoonPioneerClone.ItemsPlacement.Core;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Movers
{
    [Serializable]
    public sealed class PlacingPlacementItemsMover : PlacementItemsMover
    {
        [SerializeField] private Transform itemsParent;
        
        
        public override void MoveItem(PlacementItem item, Vector3 position)
        {
            item.transform.SetParent(itemsParent);
            base.MoveItem(item, position);
        }
    }
}