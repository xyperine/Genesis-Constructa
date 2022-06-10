using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacement.Core.Area
{
    public sealed class PlacementAreaDrawer : MonoBehaviour
    {
        [SerializeField] private bool alwaysDraw;
        
        private PlacementAreaUpgradeableProperties _upgradeableProperties;

        private Vector3 _size;
        private Vector3 _center;


        private void Start()
        {
            GetUpgradeableProperties();
        }


        private void OnValidate()
        {
            GetUpgradeableProperties();
            
            TryRecalculateShape();
        }


        private void GetUpgradeableProperties()
        {
            if (TryGetComponent(out PlacementArea area))
            {
                _upgradeableProperties = area.GetUpgradeableData();
            }
        }


        private void TryRecalculateShape()
        {
            if (_upgradeableProperties == null)
            {
                return;
            }
            
            if (_upgradeableProperties.ScaledAreaSize == _size)
            {
                return;
            }
            
            _size = _upgradeableProperties.ScaledAreaSize;
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