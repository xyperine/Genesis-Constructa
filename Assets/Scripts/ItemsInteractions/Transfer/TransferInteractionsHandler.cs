using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public sealed class TransferInteractionsHandler : ItemsInteractionsHandler
    {
        [SerializeField] private TransferStackZoneBehavior transferBehavior;


        public void TransferItemsTo(TransferTarget target)
        {
            if (!validator.CanTransferTo(target))
            {
                return;
            }

            transferBehavior.TransferTo(target);
        }
    }
}