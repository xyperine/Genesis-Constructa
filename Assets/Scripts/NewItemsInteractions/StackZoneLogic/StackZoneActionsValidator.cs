using MoonPioneerClone.NewItemsInteractions.PickUp;
using MoonPioneerClone.NewItemsInteractions.Transfer;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.StackZoneLogic
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

            return hasItems && canTakeMore;
        }


        public bool CanTakeFrom(StackZone from)
        {
            bool fromHasItems = from.HasItems;

            return fromHasItems;
        }
	}
}