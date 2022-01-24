using MoonPioneerClone.ItemsInteractions.PickUp;
using MoonPioneerClone.ItemsInteractions.Transfer;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(StackZonesActionsValidator))]
    public sealed class ResourcesInteractionsHandler : MonoBehaviour
    {
        [SerializeField] private PickUpStackZoneBehavior pickUpBehavior;
        [SerializeField] private TransferStackZoneBehavior transferBehavior;
        
        private StackZonesActionsValidator _validator;


        private void Awake()
        {
            GetComponents();
        }


        private void GetComponents()
        {
            _validator = GetComponent<StackZonesActionsValidator>();
        }


        public void PickUpItem(ZoneItem item)
        {
            if (item.Moving)
            {
                return;
            }
            
            StackZone itemZone = item.Zone;
            
            if (!_validator.ZoneCanTakeItem(item))
            {
                return;
            }

            if (!itemZone)
            {
                pickUpBehavior.Add(item);
                return;
            }
            
            if (!_validator.CanTakeFrom(itemZone))
            {
                return;
            }
            
            pickUpBehavior.PickUpItem(item);
        }


        public void TransferItemsTo(ITransferTarget target)
        {
            if (!_validator.CanTransferTo(target))
            {
                return;
            }

            transferBehavior.TransferTo(target);
        }
    }
}