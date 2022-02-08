using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Null
{
    [CreateAssetMenu(fileName = "Null_Placement_Settings", menuName = "World Placement Settings/Null")]
    public sealed class NullPlacementSettingsSO : WorldPlacementSettingsSO
    {
        protected override void SetMaxItems()
        {
            MaxItems = 0;
        }
    }
}