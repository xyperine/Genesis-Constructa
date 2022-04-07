using System;
using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem
{
    public class PlayerInteractionsResolver : InteractionsResolver<PlayerInteractionsSO>
    {
        [SerializeField] private TransferTarget playerZone;
        
        
        private void Awake()
        {
            interactions.SetPlayer(playerZone);
        }


        public override bool CanTransferTo(TransferTarget target)
        {
            return interactions.CanTransferTo(target);
        }


        public override bool CanPickUpFrom(StackZone zone)
        {
            return interactions.CanPickUpFrom(zone);
        }
    }
}