using System;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsPlacement.Core.Area;
using GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.SetupSystem.StackZones
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
        [ShowIf(nameof(interactWithOthers))]
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 8)]
        [Range(0.1f, 10f)]
        private float scanRadius;

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
        [LabelWidth(300f)]
        private bool upgradeableOnItsOwn;

        [SerializeField] 
        [ShowIf(nameof(upgradeableOnItsOwn))]
        [Indent]
        private StackZoneUpgradesChainSO upgradesChain;

        public PlacementAreaSettingsSO PlacementSettings => placementSettings;
        
        public ItemType[] AcceptableItems => acceptableItems;

        public bool InteractWithOthers => interactWithOthers;
        public InteractionsList Interactions => interactions;
        public float ScanRadius => scanRadius;

        public bool InteractWithPlayer => interactWithPlayer;
        public PlayerInteractionsSO PlayerInteractionsSO => playerInteractionsSO;
        public InteractionType InteractionWithPlayerType => interactionWithPlayerType;
        
        public bool UpgradeableOnItsOwn => upgradeableOnItsOwn;
        public StackZoneUpgradesChainSO UpgradesChain => upgradesChain;


        public StackZoneSetupData()
        {
            
        }


        public StackZoneSetupData(StackZoneSetupData data)
        {
            placementSettings = data.placementSettings;
            acceptableItems = data.acceptableItems;
            interactWithOthers = data.interactWithOthers;
            interactions = data.interactions;
            scanRadius = data.scanRadius;
            interactWithPlayer = data.interactWithPlayer;
            playerInteractionsSO = data.playerInteractionsSO;
            interactionWithPlayerType = data.interactionWithPlayerType;
            upgradeableOnItsOwn = data.upgradeableOnItsOwn;
            upgradesChain = data.upgradesChain;
        }
    }
}