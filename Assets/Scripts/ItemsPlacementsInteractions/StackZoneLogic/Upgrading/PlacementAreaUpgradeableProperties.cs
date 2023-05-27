using GenesisConstructa.ItemsPlacement.Core.Area;
using GenesisConstructa.Utility.Extensions;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    public class PlacementAreaUpgradeableProperties
    {
        private readonly Vector3 _itemDimensions;
        
        private Vector3 _mirroredAreaSize;
        
        public int MaxItems { get; private set; }
        public Vector3 AreaSize { get; private set; }
        public Vector3 MirroredAreaDimensions => Vector3.Scale(_mirroredAreaSize, _itemDimensions).Abs();


        public PlacementAreaUpgradeableProperties(PlacementAreaSettingsSO areaSettings)
        {
            if (!areaSettings)
            {
                return;
            }
            
            _itemDimensions = areaSettings.ItemDimensions;

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