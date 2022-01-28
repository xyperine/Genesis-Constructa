using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Null
{
    [CreateAssetMenu(fileName = "NullPlacementSettings", menuName = "World Placement Settings/One Point")]
    public sealed class NullPlacementSettingsSO : WorldPlacementSettingsSO
    {
        protected override void SetMaxItems()
        {
            MaxItems = 0;
        }
    }
}