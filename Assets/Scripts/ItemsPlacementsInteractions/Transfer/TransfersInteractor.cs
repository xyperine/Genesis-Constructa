using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<InteractionTargetReference>
    {
        [SerializeField] private TransferStackZoneBehaviour transferBehaviour;

        protected override bool CanScan => transferBehaviour.CanGive;

        
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