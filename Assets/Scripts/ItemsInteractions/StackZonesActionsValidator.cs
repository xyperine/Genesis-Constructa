using MoonPioneerClone.ItemsInteractions.PickUp;
using MoonPioneerClone.ItemsInteractions.Transfer;
using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    public sealed class StackZonesActionsValidator : MonoBehaviour
    {
       [SerializeField] private StackZone zone;
       [SerializeField] private PickUpStackZoneBehavior pickUp;
       [SerializeField] private TransferStackZoneBehavior transfer;
       
       
       public bool ZoneCanTakeItem(ZoneItem item)
       {
           bool canTakeMore = zone.CanTakeMore;
           bool canTakeThisResource = zone.CanTakeThisResource(item.Type);

           return canTakeMore && canTakeThisResource;
       }
       
       
       public bool CanTransferTo(TransferTarget to)
       {
           bool hasItems = zone.HasItems;
           bool canTakeMore = to.CanTakeMore;
           bool canGiveTo = transfer.CanGiveTo(to);

           return hasItems && canTakeMore && canGiveTo;
       }
       
       
       public bool CanTakeFrom(StackZone from)
       {
           bool fromHasItems = from.HasItems;
           bool canPickUpFrom = pickUp.CanPickUpFrom(from);
           bool differentZones = zone != from;
            
           return fromHasItems && canPickUpFrom && differentZones;
       }

    }
}