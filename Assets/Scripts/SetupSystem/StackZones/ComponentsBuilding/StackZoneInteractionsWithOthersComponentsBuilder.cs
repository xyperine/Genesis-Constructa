using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using MoonPioneerClone.ItemsPlacementsInteractions.PickUp;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer;
using MoonPioneerClone.SetupSystem.StackZones.Markers;
using MoonPioneerClone.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MoonPioneerClone.SetupSystem.StackZones.ComponentsBuilding
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
                rootGameObject.GetGameObjectByMarker(typeof(InteractionsWithOthersSetupMarker));

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
                rootGameObject.GetGameObjectByMarker(typeof(InteractionsWithOthersSetupMarker));

            _establisher = objForInteractionsWithOthers.GetComponent<DefaultInteractionsEstablisher>();
            Rigidbody rigidBody = objForInteractionsWithOthers.GetComponent<Rigidbody>();
            
            data.Interactions.SetHolder(zone);
            _establisher.Setup(data.Interactions);
            rigidBody.useGravity = false;
        }


        private void SetupBehaviours()
        {
            RestoreMissingComponents(typeof(StackZoneBehavioursSetupMarker));

            GameObject objForInteractionsWithOthers =
                rootGameObject.GetGameObjectByMarker(typeof(InteractionsWithOthersSetupMarker));
            GameObject objForBehaviours = rootGameObject.GetGameObjectByMarker(typeof(StackZoneBehavioursSetupMarker));
            
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
                transfersInteractor.Setup(_establisher, transferBehaviour);
                
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
                pickUpsInteractor.Setup(_establisher, pickupBehaviour);
                
                return;
            }
            
            Object.DestroyImmediate(pickupBehaviour);
            Object.DestroyImmediate(pickUpsInteractor);
        }
    }
}