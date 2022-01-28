using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Grid
{
    public class GridPlacementAreaDrawer : WorldPlacementAreaDrawer<GridPlacementSettingsSO>
    {
        private Vector3 _size;
        private Vector3 _center;
        
        
        protected override void RecalculateShape()
        {
            _size = placementSettings.ScaledSize;
            _center = _size * 0.5f;
        }


        protected override void DrawShape()
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            
            Gizmos.DrawWireCube(_center, _size);
        }
    }
}