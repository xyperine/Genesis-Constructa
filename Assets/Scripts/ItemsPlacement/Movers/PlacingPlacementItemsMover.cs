﻿using System;
using GenesisConstructa.ItemsPlacement.Core;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacement.Movers
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