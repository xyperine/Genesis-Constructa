using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<TransferTargetReference>
    {
        [SerializeField] private TransferStackZoneBehaviour transferBehaviour;
        

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
            if (!establisher.CanTransferTo(reference.Target))
            {
                return;
            }
            
            TransferItemsTo(reference.Target);
        }
        
        
        public void TransferItemsTo(TransferTarget target)
        {
            if (!validator.CanTransferTo(target))
            {
                return;
            }

            transferBehaviour.TransferTo(target);
        }
    }
}