using ColonizationMobileGame.ItemsPlacement.Core.Area;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    public class PlacementAreaUpgradeableProperties
    {
        private readonly Vector3 _alignedItemSize;
        
        public int MaxItems { get; private set; }
        public Vector3 AreaSize { get; private set; }
        public Vector3 ScaledAreaSize { get; private set; }


        public PlacementAreaUpgradeableProperties(PlacementAreaSettingsSO areaSettings)
        {
            if (!areaSettings)
            {
                return;
            }
            
            _alignedItemSize = areaSettings.AlignedItemSize;

            MaxItems = areaSettings.MaxItems;
            AreaSize = areaSettings.AreaSize;
            ScaledAreaSize = areaSettings.ScaledAreaSize;
        }


        public void Upgrade(int newMaxItems)
        {
            MaxItems = newMaxItems;
            
            float itemsPerY = AreaSize.x * AreaSize.z;
            AreaSize = new Vector3(AreaSize.x, Mathf.Ceil(newMaxItems / itemsPerY), AreaSize.z);
            
            ScaledAreaSize = Vector3.Scale(AreaSize, _alignedItemSize);
        }
    }
}