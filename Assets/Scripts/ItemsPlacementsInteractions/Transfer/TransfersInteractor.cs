using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Transfer
{
    public class TransfersInteractor : StackZoneInteractor<InteractionTargetReference>
    {
        [SerializeField] private TransferStackZoneBehaviour transferBehaviour;
        

        private void OnTriggerStay(Collider other)
        {
            InteractionTargetReference reference;
            if (!other.TryGetComponent(out reference))
            {
                return;
            }

            InteractWith(reference);
        }


        protected override void InteractWith(InteractionTargetReference reference)
        {
            if (!establisher.CanTransferTo(reference.Target))
            {
                return;
            }
            
            TransferItemsTo(reference.Target);
        }
        
        
        public void TransferItemsTo(InteractionTarget target)
        {
            if (!validator.CanTransferTo(target))
            {
                return;
            }

            transferBehaviour.TransferTo(target);
        }
    }
}