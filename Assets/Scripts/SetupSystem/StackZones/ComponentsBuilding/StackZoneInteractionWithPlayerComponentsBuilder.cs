using GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.SetupSystem.StackZones.Markers;
using GenesisConstructa.Utility.Extensions;
using UnityEngine;

namespace GenesisConstructa.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneInteractionWithPlayerComponentsBuilder : StackZoneComponentsBuilder
    {
        public StackZoneInteractionWithPlayerComponentsBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab) : base(rootGameObject, zoneSchemePrefab)
        {
        }


        public override void Build(StackZoneSetupData data, StackZone zone)
        {
            base.Build(data, zone);

            RestoreMissingComponents(typeof(InteractionWithPlayerSetupMarker));

            GameObject objForInteractionWithPlayerSetup =
                rootGameObject.GetChildByMarker(typeof(InteractionWithPlayerSetupMarker));

            ConfigurableInteractionWithPlayerSetup interactionWithPlayerSetup =
                objForInteractionWithPlayerSetup.GetComponent<ConfigurableInteractionWithPlayerSetup>();

            interactionWithPlayerSetup.Setup(data.PlayerInteractionsSO, data.InteractionWithPlayerType, zone);
        }
    }
}