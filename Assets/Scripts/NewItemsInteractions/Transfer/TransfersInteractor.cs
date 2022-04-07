using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.Transfer
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
            if (!resolver.CanTransferTo(reference.Target))
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