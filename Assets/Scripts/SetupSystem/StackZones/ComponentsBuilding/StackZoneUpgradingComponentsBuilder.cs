using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.SetupSystem.StackZones.Markers;
using GenesisConstructa.SetupSystem.Upgrader.StackZones;
using GenesisConstructa.Utility.Extensions;
using UnityEngine;

namespace GenesisConstructa.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneUpgradingComponentsBuilder : StackZoneComponentsBuilder
    {
        public StackZoneUpgradingComponentsBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab) : base(rootGameObject, zoneSchemePrefab)
        {
        }


        public override void Build(StackZoneSetupData data, StackZone zone)
        {
            base.Build(data, zone);

            GameObject objForUpgraderSetup = rootGameObject.GetChildByMarker(typeof(UpgraderSetupSetupMarker));
            StackZoneUpgraderSetup upgraderSetup = objForUpgraderSetup.GetComponent<StackZoneUpgraderSetup>();
            upgraderSetup.SetData(new StackZoneUpgraderSetupData(data.UpgradesChain, new[] {zone}));
        }
    }
}