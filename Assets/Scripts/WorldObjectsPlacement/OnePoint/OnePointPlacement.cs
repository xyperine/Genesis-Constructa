﻿using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.OnePoint
{
    public sealed class OnePointPlacement : WorldPlacementArea<OnePointWorldPlacementSettings>
    {
        [SerializeField] private Transform pointTransform;
        
        
        protected override Vector3 GetPositionForNewItem()
        {
            return transform.InverseTransformPoint(pointTransform.position);
        }
    }
}