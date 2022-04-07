using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Core.Area
{
    public sealed class PlacementAreaDrawer : MonoBehaviour
    {
        [SerializeField] private PlacementAreaSettingsSO placementSettings;
        [SerializeField] private bool alwaysDraw;

        private Vector3 _size;
        private Vector3 _center;
        

        private void OnValidate()
        {
            TryRecalculateShape();
        }
        
        
        private void TryRecalculateShape()
        {
            if (!placementSettings)
            {
                return;
            }
            
            if (placementSettings.ScaledAreaSize == _size)
            {
                return;
            }
            
            _size = placementSettings.ScaledAreaSize;
            _center = _size * 0.5f;
        }
        

        private void OnDrawGizmosSelected()
        {
            if (!alwaysDraw)
            {
                DrawShape();
            }
        }


        private void OnDrawGizmos()
        {
            if (alwaysDraw)
            {
                DrawShape();
            }
        }


        private void DrawShape()
        {
            TryRecalculateShape();
            
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            
            Gizmos.DrawWireCube(_center, _size);
        }
    }
}