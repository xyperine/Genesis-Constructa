using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.OnePoint
{
    [CreateAssetMenu(fileName = "OnePointWorldPlacementSettings", menuName = "World Placement Settings/One Point")]
    public class OnePointWorldPlacementSettings : WorldPlacementSettingsSO
    {
        protected override void SetMaxItems()
        {
            MaxItems = 0;
        }
    }
}