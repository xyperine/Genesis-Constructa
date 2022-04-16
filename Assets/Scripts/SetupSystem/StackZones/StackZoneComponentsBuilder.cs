using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsPlacement.Areas;
using MoonPioneerClone.ItemsPlacement.Core.Area;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.InteractionWithPlayer;
using MoonPioneerClone.ItemsPlacementsInteractions.PickUp;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer;
using MoonPioneerClone.Utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    [Serializable]
    public class StackZoneComponentsBuilder
    {
        [SerializeField] private StackZoneScheme zoneSchemePrefab;

        private StackZoneSetupData _data;
        private GameObject _zoneGameObject;
        private StackZone _zone;

        private GameObject _objForCollider;
        
        
        public void Build(GameObject zoneGameObject, StackZoneSetupData data)
        {
            if (Equals(_data, data))
            {
                return;
            }
            // Determine what was changed in data
            List<string> changedProps = DetailedComparer.Compare(_data, data);
            _data = data;
            _zoneGameObject = zoneGameObject;

            // Build based on what was changed
            Transform scheme = Object.Instantiate(zoneSchemePrefab, _zoneGameObject.transform).transform;

            BuildStructure(scheme);
            
            Object.DestroyImmediate(scheme.gameObject);
            
            // Modify components based on what was changed

            ModifyComponents();
        }


        private void BuildStructure(Transform scheme)
        {
            foreach (Transform child in scheme.GetComponentsInChildren<Transform>())
            {
                foreach (Component component in child.GetComponents<Component>())
                {
                    if (component == component.transform)
                    {
                        continue;
                    }
                    
                    Object.DestroyImmediate(component);
                }

                if (child.parent == scheme)
                {
                    child.SetParent(_zoneGameObject.transform);
                }
            }
        }


        private void ModifyComponents()
        {
            if (!_zone)
            {
                SetupStackZone();
            }
            
            if (_data.InteractWithOthers)
            {
                SetupInteractionsWithOthers();
            }

            if (_data.InteractWithPlayer)
            {
                SetupInteractionWithPlayer();
            }
            
            SetupCollider();

            if (_data.UpgradeableOnItsOwn)
            {
                SetupUpgrading();
            }
        }


        private void SetupStackZone()
        {
            GameObject objForStackZone = GetGameObjectByComponent(typeof(StackZone));
            objForStackZone.AddComponent<StaticPlacementArea>().Setup(_data.PlacementSettings);
            objForStackZone.AddComponent<PlacementAreaDrawer>();
            _zone = objForStackZone.AddComponent<StackZone>();
        }


        private void SetupInteractionsWithOthers()
        {
            GameObject objForEstablisher = GetGameObjectByComponent(typeof(DefaultInteractionsEstablisher));
            DefaultInteractionsEstablisher establisher = objForEstablisher.AddComponent<DefaultInteractionsEstablisher>();
            establisher.Setup(_data.Interactions);

            InteractionType[] distinctInteractionTypes = _data.Interactions.InteractionTypes.ToArray();
            
            if (distinctInteractionTypes.Contains(InteractionType.Transfer))
            {
                GameObject objForTransferBehaviour = GetGameObjectByComponent(typeof(TransferStackZoneBehaviour));
                TransferStackZoneBehaviour transferBehaviour = objForTransferBehaviour.AddComponent<TransferStackZoneBehaviour>();
                transferBehaviour.Setup(_zone);
                
                GameObject objForTransferInteractor = GetGameObjectByComponent(typeof(TransfersInteractor));
                objForTransferInteractor.AddComponent<TransfersInteractor>().Setup(establisher, transferBehaviour);
            }
            
            if (distinctInteractionTypes.Contains(InteractionType.PickUp))
            {
                GameObject objForPickUpBehaviour = GetGameObjectByComponent(typeof(PickUpStackZoneBehaviour));
                PickUpStackZoneBehaviour pickupBehaviour = objForPickUpBehaviour.AddComponent<PickUpStackZoneBehaviour>();
                pickupBehaviour.Setup(_zone);
                
                GameObject objForPickUpsInteractor = GetGameObjectByComponent(typeof(PickUpsInteractor));
                objForPickUpsInteractor.AddComponent<PickUpsInteractor>().Setup(establisher, pickupBehaviour);
            }
        }

        
        private void SetupInteractionWithPlayer()
        {
            GameObject objForInteractionWithPlayerSetup =
                GetGameObjectByComponent(typeof(ConfigurableInteractionWithPlayerSetup));
            ConfigurableInteractionWithPlayerSetup interactionWithPlayerSetup =
                objForInteractionWithPlayerSetup.AddComponent<ConfigurableInteractionWithPlayerSetup>();
            
            interactionWithPlayerSetup.Setup(_data.PlayerInteractionsSO, _data.InteractionWithPlayerType);
        }


        private void SetupCollider()
        {
            _objForCollider = GetGameObjectByComponent(typeof(Collider));
            Collider collider = _objForCollider.AddComponent(_data.ColliderData.SelectedColliderType) as Collider;

            if (collider != null)
            {
                collider.isTrigger = true;
            }

            InteractionTargetReference targetReference = _objForCollider.AddComponent<InteractionTargetReference>();
            targetReference.Setup(_zone);

            ConfigureCollider();
        }

        
        private void SetupUpgrading()
        {
            GameObject objForUpgrader = GetGameObjectByComponent(typeof(StackZoneUpgrader));
            StackZoneUpgrader upgrader = objForUpgrader.AddComponent<StackZoneUpgrader>();
            
            upgrader.Setup(_data.UpgradesChain, new []{_zone});
        }

        

        private GameObject GetGameObjectByComponent(Type component)
        {
            string objName = zoneSchemePrefab.GetNameOfGameObjectFor(component);
            GameObject obj = _zoneGameObject.GetComponentsInChildren<Transform>()
                .SingleOrDefault(t => Equals(Helpers.GetGameObjectPathWithoutRoot(t), objName))?.gameObject;

            return obj;
        }


        public void ConfigureCollider()
        {
            if (!_objForCollider)
            {
                Debug.LogWarning("There is no Collider gameobject");
                return;
            }
            
            Selection.activeGameObject = _objForCollider;
            SceneView.FrameLastActiveSceneView();
        }
    }
}