using System;
using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZone
{
    [Serializable]
    public class StackZoneSetupData
    {
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
        private InteractionType interactionWithPlayerType;

        [SerializeField]
        [LabelWidth(300f)]
        private bool upgradeableOnItsOwn;

        [SerializeField] 
        [ShowIf(nameof(upgradeableOnItsOwn))]
        [Indent]
        private StackZoneUpgradesChainSO upgradesChain;

        [SerializeField]
        [HideReferenceObjectPicker]
        private StackZoneColliderSetupDataPicker colliderData = new StackZoneColliderSetupDataPicker();


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