﻿using ColonizationMobileGame.ItemsPlacement.Core;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacement.Movers
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