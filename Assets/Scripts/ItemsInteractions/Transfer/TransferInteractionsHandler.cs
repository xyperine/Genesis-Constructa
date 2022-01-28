using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public sealed class TransferInteractionsHandler : ItemsInteractionsHandler
    {
        [SerializeField] private TransferStackZoneBehaviour _transferBehaviour;


        public void TransferItemsTo(TransferTarget target)
        {
            if (!validator.CanTransferTo(target))
            {
                return;
            }

            _transferBehaviour.TransferTo(target);
        }
    }
}