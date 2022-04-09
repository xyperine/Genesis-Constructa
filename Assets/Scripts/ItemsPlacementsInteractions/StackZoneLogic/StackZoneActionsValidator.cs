using MoonPioneerClone.ItemsPlacementsInteractions.PickUp;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic
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


        public bool CanTransferTo(InteractionTarget to)
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