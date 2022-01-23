using System.Collections.Generic;
using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions.Split.PickUp
{
    public class PickUpStackZoneBehavior : MonoBehaviour
    {
        [SerializeField] private StackZone stackZone;
        [SerializeField] private List<StackZone> pickUpFrom = new List<StackZone>();

        public bool CanPickUpFrom(StackZone from) => pickUpFrom.Contains(from);


        public void PickUpItem(ZoneItem item)
        {
            StackZone itemZone = item.Zone;
            
            itemZone.Remove(item);
            Add(item);
        }


        public void Add(ZoneItem item)
        {
            stackZone.Add(item);
        }
    }
}