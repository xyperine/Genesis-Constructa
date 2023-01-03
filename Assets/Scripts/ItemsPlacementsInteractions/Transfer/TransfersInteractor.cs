using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<InteractionTargetReference, TransferStackZoneBehaviour>
    {
        protected override bool CanScan => behaviour.CanGive;


        protected override void InteractWith(InteractionTargetReference reference)
        { 
            if (!establisher.CanTransferTo(reference.Target))
            {
                return;
            }
            
            behaviour.TransferTo(reference.Target);
        }
    }
}