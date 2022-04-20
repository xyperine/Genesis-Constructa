using System;
using MoonPioneerClone.ItemsPlacement.Core.Area;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones
{
    [Serializable]
    public class StackZoneSetupData : IConstructData
    {
        [SerializeField]
        private PlacementAreaSettingsSO placementSettings;

        [SerializeField] 
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 8)]
        private ItemType[] acceptableItems;
        
        [SerializeField]
        [LabelWidth(300f)]
        private bool interactWithOthers;

        [SerializeField]
        [ShowIf(nameof(interactWithOthers))]
        [HideLabel]
        [HideReferenceObjectPicker]
        [Indent]
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 8)]
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
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 8)]
        private InteractionType interactionWithPlayerType;

        [SerializeField] 
        [HideReferenceObjectPicker]
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 8)]
        private StackZoneColliderPicker colliderData;

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
        [Range(1f, 10f)]
        private float upgraderColliderRadius;
        
        public PlacementAreaSettingsSO PlacementSettings => placementSettings;
        
        public ItemType[] AcceptableItems => acceptableItems;

        public bool InteractWithOthers => interactWithOthers;
        public InteractionsList Interactions => interactions;

        public bool InteractWithPlayer => interactWithPlayer;
        public PlayerInteractionsSO PlayerInteractionsSO => playerInteractionsSO;
        public InteractionType InteractionWithPlayerType => interactionWithPlayerType;
        
        public StackZoneColliderPicker ColliderData => colliderData;

        public bool UpgradeableOnItsOwn => upgradeableOnItsOwn;
        public StackZoneUpgradesChainSO UpgradesChain => upgradesChain;

        public float UpgraderColliderRadius => upgraderColliderRadius;


        public StackZoneSetupData()
        {
            
        }


        public StackZoneSetupData(StackZoneSetupData data)
        {
            placementSettings = data.placementSettings;
            acceptableItems = data.acceptableItems;
            interactWithOthers = data.interactWithOthers;
            interactions = data.interactions;
            interactWithPlayer = data.interactWithPlayer;
            playerInteractionsSO = data.playerInteractionsSO;
            interactionWithPlayerType =data. interactionWithPlayerType;
            colliderData = data.colliderData;
            upgradeableOnItsOwn = data.upgradeableOnItsOwn;
            upgradesChain = data.upgradesChain;
            upgraderColliderRadius = data.upgraderColliderRadius;
        }
    }
}