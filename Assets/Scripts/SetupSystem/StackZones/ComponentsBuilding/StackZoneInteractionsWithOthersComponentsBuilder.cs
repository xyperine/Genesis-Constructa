using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup;
using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using ColonizationMobileGame.ItemsPlacementsInteractions.PickUp;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.ItemsPlacementsInteractions.Transfer;
using ColonizationMobileGame.SetupSystem.StackZones.Markers;
using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ColonizationMobileGame.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneInteractionsWithOthersComponentsBuilder : StackZoneComponentsBuilder
    {
        private DefaultInteractionsEstablisher _establisher;


        public StackZoneInteractionsWithOthersComponentsBuilder(GameObject rootGameObject, SetupScheme zoneSchemePrefab) : base(rootGameObject, zoneSchemePrefab)
        {
        }


        public override void Build(StackZoneSetupData data, StackZone zone)
        {
            base.Build(data, zone);

            RestoreMissingComponents(typeof(InteractionsWithOthersSetupMarker));

            if (!this.data.InteractWithOthers)
            {
                ClearInteractionsWithOthersGameObject();
                return;
            }
            
            SetupRequiredComponents();
            SetupBehaviours();
        }


        private void ClearInteractionsWithOthersGameObject()
        {
            GameObject objForInteractionsWithOthers =
                rootGameObject.GetChildByMarker(typeof(InteractionsWithOthersSetupMarker));

            Component[] componentsToPreserve = 
            {
                objForInteractionsWithOthers.transform,
                objForInteractionsWithOthers.GetComponent<InteractionsWithOthersSetupMarker>(),
            };

            IEnumerable<Component> componentsToDestroy = objForInteractionsWithOthers.GetComponents<Component>().Except(componentsToPreserve);

            foreach (Component component in componentsToDestroy)
            {
                Object.DestroyImmediate(component);
            }
        }
        

        private void SetupRequiredComponents()
        {
            GameObject objForInteractionsWithOthers =
                rootGameObject.GetChildByMarker(typeof(InteractionsWithOthersSetupMarker));

            _establisher = objForInteractionsWithOthers.GetComponent<DefaultInteractionsEstablisher>();
            InteractionTargetReference targetReference =
                objForInteractionsWithOthers.GetComponent<InteractionTargetReference>();

            data.Interactions.SetHolder(zone);
            _establisher.Setup(data.Interactions);
            targetReference.Setup(zone);
        }


        private void SetupBehaviours()
        {
            RestoreMissingComponents(typeof(StackZoneBehavioursSetupMarker));

            GameObject objForInteractionsWithOthers =
                rootGameObject.GetChildByMarker(typeof(InteractionsWithOthersSetupMarker));
            GameObject objForBehaviours = rootGameObject.GetChildByMarker(typeof(StackZoneBehavioursSetupMarker));
            
            SetupTransferBehaviour(objForInteractionsWithOthers, objForBehaviours);
            SetupPickUpBehaviour(objForInteractionsWithOthers, objForBehaviours);
        }


        private void SetupTransferBehaviour(GameObject objForInteractionsWithOthers, GameObject objForBehaviours)
        {
            TransferStackZoneBehaviour transferBehaviour = objForBehaviours.GetComponent<TransferStackZoneBehaviour>();
            TransfersInteractor transfersInteractor = objForInteractionsWithOthers.GetComponent<TransfersInteractor>();

            if (data.Interactions.InteractionTypes.Contains(InteractionType.Transfer))
            {
                transferBehaviour.Setup(zone);
                transfersInteractor.Setup(_establisher, transferBehaviour, data.ScanRadius);
                
                return;
            }
            
            Object.DestroyImmediate(transferBehaviour);
            Object.DestroyImmediate(transfersInteractor);
        }


        private void SetupPickUpBehaviour(GameObject objForInteractionsWithOthers, GameObject objForBehaviours)
        {
            PickUpStackZoneBehaviour pickupBehaviour = objForBehaviours.GetComponent<PickUpStackZoneBehaviour>();
            PickUpsInteractor pickUpsInteractor = objForInteractionsWithOthers.GetComponent<PickUpsInteractor>();

            if (data.Interactions.InteractionTypes.Contains(InteractionType.PickUp))
            {
                pickupBehaviour.Setup(zone);
                pickUpsInteractor.Setup(_establisher, pickupBehaviour, data.ScanRadius);
                
                return;
            }
            
            Object.DestroyImmediate(pickupBehaviour);
            Object.DestroyImmediate(pickUpsInteractor);
        }
    }
}