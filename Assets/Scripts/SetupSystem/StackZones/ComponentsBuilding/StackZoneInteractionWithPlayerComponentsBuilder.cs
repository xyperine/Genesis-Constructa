using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.SetupSystem.StackZones.Markers;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneInteractionWithPlayerComponentsBuilder : StackZoneComponentsBuilder
    {
        public StackZoneInteractionWithPlayerComponentsBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab) : base(rootGameObject, zoneSchemePrefab)
        {
        }


        public override void Setup(StackZoneSetupData data, StackZone zone)
        {
            base.Setup(data, zone);

            RestoreMissingComponents(typeof(InteractionWithPlayerSetupMarker));

            GameObject objForInteractionWithPlayerSetup =
                rootGameObject.GetGameObjectByMarker(typeof(InteractionWithPlayerSetupMarker));

            ConfigurableInteractionWithPlayerSetup interactionWithPlayerSetup =
                objForInteractionWithPlayerSetup.GetComponent<ConfigurableInteractionWithPlayerSetup>();

            interactionWithPlayerSetup.Setup(data.PlayerInteractionsSO, data.InteractionWithPlayerType, zone);
        }
    }
}