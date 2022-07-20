using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.SetupSystem.StackZones.Markers;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.StackZones.ComponentsBuilding
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