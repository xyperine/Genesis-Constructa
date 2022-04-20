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
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer;
using MoonPioneerClone.SetupSystem.Upgrader.StackZones;
using MoonPioneerClone.Utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    [Serializable]
    public class StackZoneComponentsBuilder
    {
        [SerializeField] private SetupScheme zoneSchemePrefab;
        
        private StackZoneSetupData _data;
        private GameObject _zoneGameObject;
        private StackZone _zone;

        private GameObject _objForCollider;

        private readonly List<Component> _componentsToRemove = new List<Component>();
        private readonly List<GameObject> _objectsToRemove = new List<GameObject>();


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
            
            SetupCollider();
            
            SetupInteractionsWithOthers();
            
            SetupInteractionWithPlayer();
            
            SetupUpgrading();

            foreach (GameObject obj in _objectsToRemove)
            {
                if (obj)
                {
                    Object.DestroyImmediate(obj);
                }
            }
            
            foreach (Component component in _componentsToRemove)
            {
                if (component && component != component.transform)
                {
                    Object.DestroyImmediate(component);
                }
            }
        }


        private void SetupStackZone()
        {
            GameObject objForStackZone = GetGameObjectByComponent(typeof(StackZone));
            objForStackZone.GetComponent<StaticPlacementArea>().Setup(_data.PlacementSettings);
            objForStackZone.GetComponent<PlacementAreaDrawer>();
            
            _zone = objForStackZone.GetComponent<StackZone>();
            _zone.Setup(_data.AcceptableItems);
        }


        private void SetupInteractionsWithOthers()
        {
            GameObject objForEstablisher = GetGameObjectByComponent(typeof(DefaultInteractionsEstablisher));
            DefaultInteractionsEstablisher establisher = objForEstablisher.GetComponent<DefaultInteractionsEstablisher>();

            GameObject objForRigidbody = GetGameObjectByComponent(typeof(Rigidbody));
            Rigidbody rigidBody = objForRigidbody.GetComponent<Rigidbody>();
            
            GameObject objForTransferBehaviour = GetGameObjectByComponent(typeof(TransferStackZoneBehaviour));
            TransferStackZoneBehaviour transferBehaviour = objForTransferBehaviour.GetComponent<TransferStackZoneBehaviour>();
                
            GameObject objForTransferInteractor = GetGameObjectByComponent(typeof(TransfersInteractor));
            TransfersInteractor transfersInteractor = objForTransferInteractor.GetComponent<TransfersInteractor>();
            
            GameObject objForPickUpBehaviour = GetGameObjectByComponent(typeof(PickUpStackZoneBehaviour));
            PickUpStackZoneBehaviour pickupBehaviour = objForPickUpBehaviour.GetComponent<PickUpStackZoneBehaviour>();
                
            GameObject objForPickUpsInteractor = GetGameObjectByComponent(typeof(PickUpsInteractor));
            PickUpsInteractor pickUpsInteractor = objForPickUpsInteractor.GetComponent<PickUpsInteractor>();
            
            if (!_data.InteractWithOthers)
            {
                _componentsToRemove.Add(establisher);
                _componentsToRemove.Add(rigidBody);
                _componentsToRemove.Add(transfersInteractor);
                _componentsToRemove.Add(pickUpsInteractor);
                
                _objectsToRemove.Add(objForTransferBehaviour);
                _objectsToRemove.Add(objForPickUpBehaviour);
                
                return;
            }
            
            _data.Interactions.SetHolder(_zone);
            establisher.Setup(_data.Interactions);
            rigidBody.useGravity = false;
            
            InteractionType[] distinctInteractionTypes = _data.Interactions.InteractionTypes.ToArray();

            if (distinctInteractionTypes.Contains(InteractionType.Transfer))
            {
                transferBehaviour.Setup(_zone);
                transfersInteractor.Setup(establisher, transferBehaviour);
            }
            else
            {
                _componentsToRemove.Add(transferBehaviour);
                _componentsToRemove.Add(transfersInteractor);
            }

            if (distinctInteractionTypes.Contains(InteractionType.PickUp))
            {
                pickupBehaviour.Setup(_zone);
                pickUpsInteractor.Setup(establisher, pickupBehaviour);
            }
            else
            {
                _componentsToRemove.Add(pickupBehaviour);
                _componentsToRemove.Add(pickUpsInteractor);
            }
        }

        
        private void SetupInteractionWithPlayer()
        {
            GameObject objForInteractionWithPlayerSetup =
                GetGameObjectByComponent(typeof(ConfigurableInteractionWithPlayerSetup));
            ConfigurableInteractionWithPlayerSetup interactionWithPlayerSetup =
                objForInteractionWithPlayerSetup.GetComponent<ConfigurableInteractionWithPlayerSetup>();
            
            if (!_data.InteractWithPlayer)
            {
                _componentsToRemove.Add(interactionWithPlayerSetup);
                
                return;
            }
            
            interactionWithPlayerSetup.Setup(_data.PlayerInteractionsSO, _data.InteractionWithPlayerType, _zone);
        }


        private void SetupCollider()
        {
            _objForCollider = GetGameObjectByComponent(typeof(Collider));
            Collider[] colliders = _objForCollider.GetComponents<Collider>();
            Collider collider = colliders.SingleOrDefault(c => c.GetType() == _data.ColliderData.SelectedColliderType);

            if (!collider)
            {
                Debug.LogError("No colliders available!");
                return;
            }
            
            collider.isTrigger = true;
            
            _componentsToRemove.AddRange(colliders.Except(new[] {collider}));

            InteractionTargetReference targetReference = _objForCollider.GetComponent<InteractionTargetReference>();
            targetReference.Setup(_zone);

            if (_data.ColliderData.ConfigureCollider)
            {
                ConfigureCollider();
            }
        }

        
        private void SetupUpgrading()
        {
            GameObject objForUpgraderSetup = GetGameObjectByComponent(typeof(StackZoneUpgraderSetup));
            
            if (!_data.UpgradeableOnItsOwn)
            {
                _objectsToRemove.Add(objForUpgraderSetup);
                
                return;
            }
            
            StackZoneUpgraderSetup upgraderSetup = objForUpgraderSetup.GetComponent<StackZoneUpgraderSetup>();

            upgraderSetup.SetData(new StackZoneUpgraderSetupData(_data.UpgradesChain, new[] {_zone},
                _data.UpgraderColliderRadius));
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