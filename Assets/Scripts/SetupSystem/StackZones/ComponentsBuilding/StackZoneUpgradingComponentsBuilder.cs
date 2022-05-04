using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.SetupSystem.StackZones.Markers;
using MoonPioneerClone.SetupSystem.Upgrader.StackZones;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneUpgradingComponentsBuilder : StackZoneComponentsBuilder
    {
        public StackZoneUpgradingComponentsBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab) : base(rootGameObject, zoneSchemePrefab)
        {
        }


        public override void Build(StackZoneSetupData data, StackZone zone)
        {
            base.Build(data, zone);

            GameObject objForUpgraderSetup = rootGameObject.GetGameObjectByMarker(typeof(UpgraderSetupSetupMarker));
            StackZoneUpgraderSetup upgraderSetup = objForUpgraderSetup.GetComponent<StackZoneUpgraderSetup>();

            upgraderSetup.SetData(new StackZoneUpgraderSetupData(data.UpgradesChain, new[] {zone},
                data.UpgraderColliderRadius));
        }
    }
}