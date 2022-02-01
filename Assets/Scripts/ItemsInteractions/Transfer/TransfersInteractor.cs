using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<TransferTargetReference, TransferInteractionsHandler>
    {
        private void OnTriggerStay(Collider other)
        {
            TransferTargetReference reference;
            if (!other.TryGetComponent(out reference))
            {
                return;
            }

            InteractWith(reference);
        }


        protected override void InteractWith(TransferTargetReference reference)
        {
            handler.TransferItemsTo(reference.Target);
        }
    }
}