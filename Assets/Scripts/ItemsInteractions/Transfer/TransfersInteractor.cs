using MoonPioneerClone.ItemsInteractions.Transfer.Target;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<TransferTargetReference, TransferInteractionsHandler>
    {
        protected override void InteractWith(TransferTargetReference reference)
        {
            handler.TransferItemsTo(reference.Target);
        }
    }
}