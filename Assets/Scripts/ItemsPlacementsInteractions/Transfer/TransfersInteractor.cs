﻿using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<InteractionTargetReference>
    {
        [SerializeField] private TransferStackZoneBehaviour transferBehaviour;


        public void Setup(InteractionsEstablisher establisher, TransferStackZoneBehaviour transferBehaviour)
        {
            this.establisher = establisher;
            this.transferBehaviour = transferBehaviour;
        }


        protected override void InteractWith(InteractionTargetReference reference)
        {
            if (!establisher.CanTransferTo(reference.Target))
            {
                return;
            }
            
            transferBehaviour.TransferTo(reference.Target);
        }
    }
}