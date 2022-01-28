using System.Collections.Generic;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.PickUp
{
    public class PickUpStackZoneBehaviour : MonoBehaviour
    {
        [SerializeField] private StackZone stackZone;
        [SerializeField] private List<StackZone> pickUpFrom = new List<StackZone>();

        public bool CanPickUpFrom(StackZone from) => pickUpFrom.Contains(from);


        public void PickUpItem(StackZoneItem item)
        {
            StackZone itemZone = item.Zone;
            
            itemZone.Remove(item);
            Add(item);
        }


        public void Add(StackZoneItem item)
        {
            stackZone.Add(item);
        }
    }
}