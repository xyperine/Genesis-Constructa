using MoonPioneerClone.ItemsInteractions.PickUp;
using MoonPioneerClone.ItemsInteractions.Transfer;
using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.StackZoneLogic
{
	public sealed class StackZoneActionsValidator : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone zone; 
        [SerializeField] private PickUpStackZoneBehaviour pickUpBehaviour;
	    [SerializeField] private TransferStackZoneBehaviour transferBehaviour;
	   
	   
	    public bool ZoneCanTakeItem(StackZoneItem item)
	    {
		    bool canTakeMore = zone.CanTakeMore;
		    bool canTakeThisItem = zone.CanTakeThisItem(item.Type);

            return canTakeMore && canTakeThisItem;
	    }


        public bool CanTransferTo(TransferTarget to)
        {
            bool hasItems = zone.HasItems;
            bool canTakeMore = to.CanTakeMore;
            bool canGiveTo = transferBehaviour.CanGiveTo(to);

            return hasItems && canTakeMore && canGiveTo;
        }


        public bool CanTakeFrom(StackZone from)
        {
            bool fromHasItems = from.HasItems;
            bool canPickUpFrom = pickUpBehaviour.CanPickUpFrom(from);

            return fromHasItems && canPickUpFrom;
        }
	}
}