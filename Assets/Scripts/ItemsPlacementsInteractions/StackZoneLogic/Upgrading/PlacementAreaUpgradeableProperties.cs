using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    public class PlacementAreaUpgradeableProperties
    {
        private readonly Vector3 _alignedItemSize;
        
        private Vector3 _mirroredAreaSize;
        
        public int MaxItems { get; private set; }
        public Vector3 AreaSize { get; private set; }
        public Vector3 ScaledAreaSize => Vector3.Scale(_mirroredAreaSize, _alignedItemSize).Abs();


        public PlacementAreaUpgradeableProperties(PlacementAreaSettingsSO areaSettings)
        {
            if (!areaSettings)
            {
                return;
            }
            
            _alignedItemSize = areaSettings.AlignedItemSize;

            MaxItems = areaSettings.MaxItems;
            AreaSize = areaSettings.AreaSize;
            
            _mirroredAreaSize = -AreaSize.XZPlane() + Vector3.up * AreaSize.y;
        }


        public void Upgrade(int newMaxItems)
        {
            MaxItems = newMaxItems;
            
            float itemsPerY = AreaSize.x * AreaSize.z;
            AreaSize = new Vector3(AreaSize.x, Mathf.Ceil(newMaxItems / itemsPerY), AreaSize.z);
            
            _mirroredAreaSize = -AreaSize.XZPlane() - Vector3.up * AreaSize.y;
        }
    }
}