using System;
using MoonPioneerClone.ItemsPlacement.Core.Area;
using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    [Serializable]
    public class StackZoneSetupData
    {
        [SerializeField]
        private PlacementAreaSettingsSO placementSettings;

        [SerializeField] 
        private ItemType[] acceptableItems;
        
        [SerializeField]
        [LabelWidth(300f)]
        private bool interactWithOthers;

        [SerializeField]
        [ShowIf(nameof(interactWithOthers))]
        [HideLabel]
        [HideReferenceObjectPicker]
        [Indent]
        private InteractionsList interactions;

        [SerializeField]
        [LabelWidth(300f)]
        private bool interactWithPlayer;

        [SerializeField] 
        [ShowIf(nameof(interactWithPlayer))]
        [Indent]
        private PlayerInteractionsSO playerInteractionsSO;
        
        [SerializeField]
        [ShowIf(nameof(interactWithPlayer))]
        [Indent]
        private InteractionType interactionWithPlayerType;

        [SerializeField]
        [LabelWidth(300f)]
        private bool upgradeableOnItsOwn;

        [SerializeField] 
        [ShowIf(nameof(upgradeableOnItsOwn))]
        [Indent]
        private StackZoneUpgradesChainSO upgradesChain;

        [SerializeField]
        [ShowIf(nameof(upgradeableOnItsOwn))]
        [Indent]
        private ItemsConsumer consumerPrefab;
        
        [SerializeField] 
        [HideReferenceObjectPicker]
        private StackZoneColliderPicker colliderData;

        public PlacementAreaSettingsSO PlacementSettings => placementSettings;
        
        public ItemType[] AcceptableItems => acceptableItems;

        public bool InteractWithOthers => interactWithOthers;
        public InteractionsList Interactions => interactions;

        public bool InteractWithPlayer => interactWithPlayer;
        public PlayerInteractionsSO PlayerInteractionsSO => playerInteractionsSO;
        public InteractionType InteractionWithPlayerType => interactionWithPlayerType;

        public bool UpgradeableOnItsOwn => upgradeableOnItsOwn;
        public StackZoneUpgradesChainSO UpgradesChain => upgradesChain;
        public ItemsConsumer ConsumerPrefab => consumerPrefab;

        public StackZoneColliderPicker ColliderData => colliderData;


        public StackZoneSetupData()
        {
            
        }
        
        
        public StackZoneSetupData(StackZoneSetupData data)
        {
            interactWithOthers = data.interactWithOthers;
            interactions = data.interactions;
            interactWithPlayer = data.interactWithPlayer;
            interactionWithPlayerType = data.interactionWithPlayerType;
            upgradeableOnItsOwn = data.upgradeableOnItsOwn;
            upgradesChain = data.upgradesChain;
            colliderData = data.colliderData;
        }
    }
}